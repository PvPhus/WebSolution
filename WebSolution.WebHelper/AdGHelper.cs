using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WebSolution.WebHelper
{
    public class AdGHelper
    {
        public static string PaymentMethodIntToString(int paymentmethod)
        {
            switch (paymentmethod)
            {
                case 1:
                    return "Bank Transfer";
                case 2:
                    return "WebMoney";
                case 3:
                    return "USDT";
                case 4:
                    return "PayPal";
                default:
                    return "Main payment method not installed";
            }
        }
        public static int PaymentMethodStringToInt(string paymentmethod)
        {
            switch (paymentmethod)
            {
                case "banktransfer":
                    return 1;
                case "webmoney":
                    return 2;
                case "usdt":
                    return 3;
                case "paypal":
                    return 4;
                default:
                    return 1;
            }
        }
        public static string GetMD5(string token)
        {
            string str_md5 = "";
            byte[] mang = System.Text.Encoding.UTF8.GetBytes(token);

            MD5CryptoServiceProvider my_md5 = new MD5CryptoServiceProvider();
            mang = my_md5.ComputeHash(mang);

            foreach (byte b in mang)
            {
                str_md5 += b.ToString("X2");
            }

            return str_md5;
        }
    }
}
