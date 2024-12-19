using System;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using MongoDB.Bson;
using WebSolution.Data;
using WebSolution.Models;

namespace WebSolution.Games.Controllers
{
    public class AccountController : Controller
    {
        private static readonly AccountAdapter _accountAdapter = new AccountAdapter(CoreDataConfig.DbName);

        public ActionResult Authentication()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(string email, string password)
        {
            try
            {
                // Kiểm tra thông tin đầu vào
                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                {
                    return Json(new
                    {
                        Result = "Thông tin không đầy đủ!",
                        Error = "Trường dữ liệu rỗng",
                        Date = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")
                    }, JsonRequestBehavior.AllowGet);
                }

                // Tìm tài khoản trong MongoDB
                var account = _accountAdapter.GetAccountByEmailAndPassword(email.Trim(), password.Trim());

                if (account != null)
                {
                    Session["UserId"] = account.id;
                    Session["email"] = account.email;

                    return Json(new
                    {
                        Result = "Đăng nhập thành công!",
                        Error = "",
                        RedirectUrl = Url.Action("Index", "Home"),
                        Date = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        Result = "Tên đăng nhập hoặc mật khẩu không đúng!",
                        Error = "Xác thực thất bại",
                        Date = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Result = "Đã xảy ra lỗi không mong muốn!",
                    Error = ex.Message,
                    Date = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")
                }, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        public JsonResult Register(string username, string email, string password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                {
                    return Json(new
                    {
                        Result = "Thông tin không đầy đủ!",
                        Error = "Trường dữ liệu rỗng",
                        Date = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")
                    }, JsonRequestBehavior.AllowGet);
                }

                // Kiểm tra email đã tồn tại
                var existingAccount = _accountAdapter.GetAccountByEmail(email);
                if (existingAccount != null)
                {
                    return Json(new
                    {
                        Result = "Email đã được sử dụng!",
                        Error = "Email trùng lặp",
                        Date = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")
                    }, JsonRequestBehavior.AllowGet);
                }

                // Tạo tài khoản mới
                var account = new AccountModel
                {
                    email = email.Trim(),
                    username = username.Trim(),
                    password = password.Trim(),
                    createat = DateTime.UtcNow
                };

                // Lưu tài khoản
                var result = _accountAdapter.SaveAccount(account);

                if (result != ObjectId.Empty)
                {
                    return Json(new
                    {
                        Result = "Đăng ký thành công!",
                        Error = "",
                        RedirectUrl = Url.Action("Authentication", "Account"),
                        Date = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")
                    }, JsonRequestBehavior.AllowGet);
                }

                return Json(new
                {
                    Result = "Đăng ký thất bại!",
                    Error = "Lỗi lưu trữ dữ liệu",
                    Date = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Result = "Đã xảy ra lỗi không mong muốn!",
                    Error = ex.Message,
                    Date = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
