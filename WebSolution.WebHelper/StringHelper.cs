using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WebSolution.WebHelper
{
    public class StringHelper
    {
        private static Random random = new Random();

        public static string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
                return input;
            return input.First().ToString().ToUpper() + input.Substring(1);
        }

        public static string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
        public static string GenerateURL(object title)
        {
            string strTitle = Change_AV(title.ToString());
            try
            {
                #region Generate SEO Friendly URL based on Title
                //Trim Start and End Spaces.
                strTitle = strTitle.Trim();

                //Trim "-" Hyphen
                strTitle = strTitle.Trim('-');

                strTitle = strTitle.ToLower();
                char[] chars = @"“”$%#@!*?;:~`+=()[]{}|\'’<>,/^&"".".ToCharArray();
                strTitle = strTitle.Replace("c#", "C-Sharp");
                strTitle = strTitle.Replace("vb.net", "VB-Net");
                strTitle = strTitle.Replace("asp.net", "Asp-Net");

                //Replace . with - hyphen
                strTitle = strTitle.Replace(".", "-");

                //Replace Special-Characters
                for (int i = 0; i < chars.Length; i++)
                {
                    string strChar = chars.GetValue(i).ToString();
                    if (strTitle.Contains(strChar))
                    {
                        strTitle = strTitle.Replace(strChar, string.Empty);
                    }
                }

                //Replace all spaces with one "-" hyphen
                strTitle = strTitle.Replace(" ", "-");

                //Replace multiple "-" hyphen with single "-" hyphen.
                strTitle = strTitle.Replace("--", "-");
                strTitle = strTitle.Replace("---", "-");
                strTitle = strTitle.Replace("----", "-");
                strTitle = strTitle.Replace("-----", "-");
                strTitle = strTitle.Replace("----", "-");
                strTitle = strTitle.Replace("---", "-");
                strTitle = strTitle.Replace("--", "-");
                strTitle = strTitle.Replace("�", "");
                strTitle = strTitle.Replace(" -", "-");
                strTitle = strTitle.Replace("- ", "-");
                //Run the code again...
                //Trim Start and End Spaces.
                strTitle = strTitle.Trim();

                //Trim "-" Hyphen
                strTitle = strTitle.Trim('-');
                #endregion

                if (string.IsNullOrEmpty(strTitle))
                {
                    return RemoveSpecialCharacters(title.ToString()).Replace("�", "");
                }
                else
                {
                    return strTitle.Replace("�", "");
                }
            }
            catch
            { }
            return "tagx";
        }
        public static string GenerateLink(string beurl, object Title, object strId)
        {
            string strTitle = Change_AV(Title.ToString());

            #region Generate SEO Friendly URL based on Title
            //Trim Start and End Spaces.
            strTitle = strTitle.Trim();

            //Trim "-" Hyphen
            strTitle = strTitle.Trim('-');

            strTitle = strTitle.ToLower();
            char[] chars = @"$%#@!*?;:~`+=()[]{}|\'<>,/^&"".：‘".ToCharArray();
            strTitle = strTitle.Replace("c#", "C-Sharp");
            strTitle = strTitle.Replace("vb.net", "VB-Net");
            strTitle = strTitle.Replace("asp.net", "Asp-Net");

            //Replace . with - hyphen
            strTitle = strTitle.Replace(".", "-");

            //Replace Special-Characters
            for (int i = 0; i < chars.Length; i++)
            {
                string strChar = chars.GetValue(i).ToString();
                if (strTitle.Contains(strChar))
                {
                    strTitle = strTitle.Replace(strChar, string.Empty);
                }
            }

            //Replace all spaces with one "-" hyphen
            strTitle = strTitle.Replace(" ", "-");

            //Replace multiple "-" hyphen with single "-" hyphen.
            strTitle = strTitle.Replace("--", "-");
            strTitle = strTitle.Replace("---", "-");
            strTitle = strTitle.Replace("----", "-");
            strTitle = strTitle.Replace("-----", "-");
            strTitle = strTitle.Replace("----", "-");
            strTitle = strTitle.Replace("---", "-");
            strTitle = strTitle.Replace("--", "-");

            //Run the code again...
            //Trim Start and End Spaces.
            strTitle = strTitle.Trim();

            //Trim "-" Hyphen
            strTitle = strTitle.Trim('-');
            #endregion

            //Append ID at the end of SEO Friendly URL
            strTitle = beurl + strTitle + "-" + strId + ".html";

            return strTitle;
        }
        public static string Change_AV(string ip_str_change)
        {
            try
            {
                Regex v_reg_regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
                string v_str_FormD = ip_str_change.Normalize(NormalizationForm.FormD);
                return v_reg_regex.Replace(v_str_FormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
            }
            catch
            { }
            return "detail";
        }
        public static string FirstLetterToUpper(string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
        }
        public static string FindFirstInsert(string Source, string Find, string Replace)
        {
            try
            {
                int Place = Source.IndexOf(Find);
                if (Place > -1)
                    return Source.Insert(Place, Replace);
                else
                    return Source;
            }
            catch
            { }
            return Source;
        }
        public static string FindInsertByIndex(string Source, string Find, string Replace, int start = 0)
        {
            try
            {
                int Place = Source.IndexOf(Find, start);
                if (Place > -1)
                    return Source.Insert(Place, Replace);
                else
                    return Source;
            }
            catch
            { }
            return Source;
        }
        public static string FindLastInsert(string Source, string Find, string Replace)
        {
            try
            {
                int Place = Source.LastIndexOf(Find);
                if (Place > -1)
                    return Source.Insert(Place, Replace);
                else
                    return Source;
            }
            catch
            { }
            return Source;
        }
        //Encode
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        //Decode
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        public static bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
        public static string GetDomainFromUrl(string url)
        {
            try
            {
                Uri myUri = new Uri(url);
                return myUri.Host.ToLower();
            }
            catch
            { }
            return null;
        }
       
        public static string RandomString(int length)
        {
            const string chars = "0123456789ABCDEFGHJKM0123456789NOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static long GetIDFromUrl(string url)
        {
            string lineUrl = url;
            if (!string.IsNullOrEmpty(lineUrl) && lineUrl.Length > 30)
            {
                lineUrl = lineUrl.Trim();
                string findId = string.Empty;
                if (lineUrl.Contains(".html"))
                {
                    string[] arrUrl = lineUrl.Split('-');
                    findId = arrUrl[arrUrl.Length - 1];
                    arrUrl = findId.Split('.');
                    findId = arrUrl[0].Trim();
                }
                else
                {
                    string[] arrUrl = lineUrl.Split('/');
                    findId = arrUrl[arrUrl.Length - 1].Trim();
                }
                if (!string.IsNullOrEmpty(findId))
                {
                    if (findId.Contains("#"))
                    {
                        findId = findId.Split('#')[0];
                    }
                    if (findId.Contains("?"))
                    {
                        findId = findId.Split('?')[0];
                    }
                    long rid = 0;
                    if (long.TryParse(findId, out rid))
                    {
                        return rid;
                    }
                }
            }
            return 0;
        }
    }
}
