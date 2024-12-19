using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSolution.WebHelper
{
    public class WebConfig
    {
        public static readonly string Domain = System.Configuration.ConfigurationManager.AppSettings["Domain"];
        public static readonly string DomainName = System.Configuration.ConfigurationManager.AppSettings["DomainName"];
        public static readonly string DomainUrl = System.Configuration.ConfigurationManager.AppSettings["DomainUrl"];
        public static readonly string Layout = System.Configuration.ConfigurationManager.AppSettings["Layout"];
        public static readonly string LogoUrl = System.Configuration.ConfigurationManager.AppSettings["LogoUrl"];
     
        public static readonly string DefaultImage = System.Configuration.ConfigurationManager.AppSettings["DefaultImage"];
        public static readonly string LanguageAds = System.Configuration.ConfigurationManager.AppSettings["LanguageAds"];
        public static readonly int Language = int.Parse(System.Configuration.ConfigurationManager.AppSettings["Language"]);
        public static readonly string ImageUrl = System.Configuration.ConfigurationManager.AppSettings["ImageUrl"];
        public static readonly string GoogleAnalytics = System.Configuration.ConfigurationManager.AppSettings["GoogleAnalytics"];
        public static readonly string ColorLink = System.Configuration.ConfigurationManager.AppSettings["ColorLink"];
        public static readonly string ColorTitle = System.Configuration.ConfigurationManager.AppSettings["ColorTitle"];
        public static readonly string Title = System.Configuration.ConfigurationManager.AppSettings["Title"];

    }
}