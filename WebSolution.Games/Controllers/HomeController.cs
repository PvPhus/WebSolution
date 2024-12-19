using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSolution.Models;
using WebSolution.Data;
using MongoDB.Bson;

namespace WebSolution.Games.Controllers
{
    public class HomeController : Controller
    {
        public static AccountAdapter _accountAdapter = new AccountAdapter(CoreDataConfig.DbName);

        public ActionResult Index()
        {
            //Insert 1 account
            //Account account = new Account();
            //account.email = "test2@gmail.com";
            //account.username = "test2";
            //account.password = "999999";
            //if (_accountAdapter.SaveNow(account) > 0)
            //{
            //    //Luuw thanhf cong
            //}
            //else
            //{
            //    //Cloi                   
            //}

            ////Update 1 account
            //Account account = _accountAdapter.GetAccountsByUsername("test1");
            //if (account != null)
            //{
            //    account.password = "68686868";
            //    if (_accountAdapter.SaveNow(account) > 0)
            //    {
            //        //Luuw thanhf cong
            //    }
            //    else
            //    {
            //        //Cloi                   
            //    }
            //}

            //Lay danh sach user
            //List<Account> accounts = _accountAdapter.GetAccounts();
            return View();
        }
        public JsonResult GetListRank()
        {
            return Json(new
            {
                Name = "UrlResponse",
                Response = "Response from Get",
                Date = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveAcount(string username, string email, string password)
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
        public ActionResult TopRank()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}