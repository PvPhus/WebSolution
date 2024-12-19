using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
namespace WebSolution.WebHelper
{
    public class ClientHelper
    {
        public static string HTMLMenu;
        public string GetMD5Hash(string input)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(input);
            bs = x.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            string password = s.ToString();
            return password;
        }
        public string CheckTypeImageUpload(FileUpload fileup)
        {
            try
            {
                if (fileup.HasFile)
                {
                    if (fileup.PostedFile.ContentLength > 320001)
                    {
                        return "Kích thước ảnh quá lớn (<= 300Kb)";
                    }
                    System.Drawing.Image image = System.Drawing.Image.FromStream(fileup.FileContent);
                    string FormetType = string.Empty;
                    if (image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Jpeg.Guid)
                        return null;
                    else if (image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Gif.Guid)
                        return null;
                    else if (image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Bmp.Guid)
                        return null;
                    else if (image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Png.Guid)
                        return null;
                    else if (image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Icon.Guid)
                        return null;
                    else
                        return "Đạnh dạng không hợp lệ";

                }
                else
                    return null;
            }
            catch
            {
                return "Có lỗi khi Upload ảnh";
            }
        }
        public static bool IsMobile(HttpRequestBase request)
        {
            HttpCookie platformCookie = request.Cookies.Get("vscreen");
            if (platformCookie == null)
                if (request.Browser.IsMobileDevice) return true;
            if (request.Browser["IsMobile"] == "True")
                return true;
            if (request.UserAgent != null && request.UserAgent.IndexOf("Android", StringComparison.OrdinalIgnoreCase) > 0)
                return true;
            return false;
        }
    }
}