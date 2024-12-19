using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebSolution.Models;

namespace WebSolution.WebHelper
{
    public class WebSolutionHelper
    {
        public static int LangConfigHelper = 0;

        public static List<GameCategory> ListCategory = new List<GameCategory>();
        public static string GetAuthor(int author)
        {
            switch (author)
            {
                case 1:
                    return "Stephen Breyer";
                case 2:
                    return "Hanoway Hilton";
                case 3:
                    return "Yoni Antonio";
                default:
                    break;
            }
            return "";
        }
        public static string GetSubDomain(int language, int lc = -1)
        {
            if (lc == -1)
            {
                lc = LangConfigHelper;
            }
            if (lc == 0)
            {
                switch (language)
                {
                    case 0:
                        return "en";
                    case 1:
                        return "de";
                    case 2:
                        return "fr";
                    case 3:
                        return "es";
                    case 4:
                        return "pt";
                    case 5:
                        return "it";
                    case 6:
                        return "nl";
                    case 7:
                        return "zh";
                    case 8:
                        return "ja";
                    case 9:
                        return "kr";
                    case 10:
                        return "ar";
                    case 11:
                        return "hi";
                    case 12:
                        return "my";
                    case 13:
                        return "vi";
                    case 14:
                        return "ru";
                    case 15:
                        return "pl";
                    case 16:
                        return "fa";
                    case 17:
                        return "ro";
                    case 18:
                        return "id";
                    case 19:
                        return "tr";
                    case 20:
                        return "th";
                    default:
                        return "en";
                }
            }
            else
            {
                return GetSubDomain_EU(language);
            }
        }
        public static int GetLangBySubDomain(string subdomain, int lc = -1)
        {
            if (lc == -1)
            {
                lc = LangConfigHelper;
            }
            if (lc == 0)
            {
                switch (subdomain)
                {
                    case "en":
                        return 0;
                    case "de":
                        return 1;
                    case "fr":
                        return 2;
                    case "es":
                        return 3;
                    case "pt":
                        return 4;
                    case "it":
                        return 5;
                    case "nl":
                        return 6;
                    case "zh":
                        return 7;
                    case "ja":
                        return 8;
                    case "kr":
                        return 9;
                    case "ar":
                        return 10;
                    case "hi":
                        return 11;
                    case "my":
                        return 12;
                    case "vi":
                        return 13;
                    case "ru":
                        return 14;
                    case "pl":
                        return 15;
                    case "fa":
                        return 16;
                    case "ro":
                        return 17;
                    case "id":
                        return 18;
                    case "tr":
                        return 19;
                    case "th":
                        return 20;
                    default:
                        return 0;
                }
            }
            else
            {
                return 0;
            }
        }
        public static int GetLangByUrl(string url)
        {
            if (url.Contains("/en/"))
            {
                return 0;
            }
            else if (url.Contains("/de/") || url.Contains("//de."))
            {
                return 1;
            }
            else if (url.Contains("/fr/") || url.Contains("//fr."))
            {
                return 2;
            }
            else if (url.Contains("/es/") || url.Contains("//es."))
            {
                return 3;
            }
            else if (url.Contains("/pt/") || url.Contains("//pt."))
            {
                return 4;
            }
            else if (url.Contains("/it/") || url.Contains("//it."))
            {
                return 5;
            }
            else if (url.Contains("/nl/") || url.Contains("//nl."))
            {
                return 6;
            }
            else if (url.Contains("/zh/") || url.Contains("//zh."))
            {
                return 7;
            }
            else if (url.Contains("/ja/") || url.Contains("//ja."))
            {
                return 8;
            }
            else if (url.Contains("/kr/") || url.Contains("//kr."))
            {
                return 9;
            }
            else if (url.Contains("/ar/") || url.Contains("//ar."))
            {
                return 10;
            }
            else if (url.Contains("/hi/") || url.Contains("//hi."))
            {
                return 11;
            }
            else if (url.Contains("/my/") || url.Contains("//my."))
            {
                return 12;
            }
            else if (url.Contains("/vi/") || url.Contains("//vi."))
            {
                return 13;
            }
            else if (url.Contains("/ru/") || url.Contains("//ru."))
            {
                return 14;
            }
            else if (url.Contains("/pl/") || url.Contains("//pl."))
            {
                return 15;
            }
            else if (url.Contains("/fa/") || url.Contains("//fa."))
            {
                return 16;
            }
            else if (url.Contains("/ro/") || url.Contains("//ro."))
            {
                return 17;
            }
            else if (url.Contains("/id/") || url.Contains("//id."))
            {
                return 18;
            }
            else if (url.Contains("/tr/") || url.Contains("//tr."))
            {
                return 19;
            }
            else if (url.Contains("/th/") || url.Contains("//th."))
            {
                return 20;
            }
            else
            {
                return 0;
            }
        }
        public static string GetLanguageISO6391(int language, int lc = -1)
        {
            if (lc == -1)
            {
                lc = LangConfigHelper;
            }
            if (lc == 0)
            {
                switch (language)
                {
                    case 0:
                        return "en";
                    case 1:
                        return "de";
                    case 2:
                        return "fr";
                    case 3:
                        return "es";
                    case 4:
                        return "pt";
                    case 5:
                        return "it";
                    case 6:
                        return "nl";
                    case 7:
                        return "zh";
                    case 8:
                        return "ja";
                    case 9:
                        return "ko";
                    case 10:
                        return "ar";
                    case 11:
                        return "hi";
                    case 12:
                        return "ms";
                    case 13:
                        return "vi";
                    case 14:
                        return "ru";
                    case 15:
                        return "pl";
                    case 16:
                        return "fa";
                    case 17:
                        return "ro";
                    case 18:
                        return "id";
                    case 19:
                        return "tr";
                    case 20:
                        return "th";
                    default:
                        return "en";
                }
            }
            else
            {
                return GetLanguageISO6391_EU(language);
            }
        }
        public static string GetSubDomain_EU(int language)
        {
            switch (language)
            {
                case 0:
                    return "en";
                case 1:
                    return "ca";
                case 2:
                    return "uk";
                case 3:
                    return "no";
                case 4:
                    return "hu";
                case 5:
                    return "el";
                case 6:
                    return "cz";
                case 7:
                    return "sv";
                case 8:
                    return "sr";
                case 9:
                    return "bg";
                case 10:
                    return "is";
                case 11:
                    return "hr";
                case 12:
                    return "da";
                case 13:
                    return "al";
                case 14:
                    return "fi";
                case 15:
                    return "sk";
                case 16:
                    return "lt";
                case 17:
                    return "lv";
                case 18:
                    return "et";
                case 19:
                    return "si";
                case 20:
                    return "gl";
                default:
                    return "en";
            }
        }
        public static string GetLanguageISO6391_EU(int language)
        {
            switch (language)
            {
                case 0:
                    return "en";
                case 1:
                    return "ca";
                case 2:
                    return "uk";
                case 3:
                    return "no";
                case 4:
                    return "hu";
                case 5:
                    return "el";
                case 6:
                    return "cz";
                case 7:
                    return "sv";
                case 8:
                    return "sr";
                case 9:
                    return "bg";
                case 10:
                    return "is";
                case 11:
                    return "hr";
                case 12:
                    return "da";
                case 13:
                    return "al";
                case 14:
                    return "fi";
                case 15:
                    return "sk";
                case 16:
                    return "lt";
                case 17:
                    return "lv";
                case 18:
                    return "et";
                case 19:
                    return "si";
                case 20:
                    return "gl";
                default:
                    return "en";
            }
        }
        public static string GetHomepageUrl(int language, int homepageLanguage = 0, int lc = -1)
        {
            if (language == homepageLanguage)
            {
                return "/";
            }
            else
            {
                return "/" + GetSubDomain(language, lc) + "/";
            }
        }
        //public static string GetCategoryUrl(int language, int id)
        //{
        //    var cat = ListCategory.FirstOrDefault(x => x.id == id);
        //    if (cat != null)
        //    {
        //        if (cat.categoryLanguage.Any(x => x.language == language))
        //        {
        //            return cat.categoryLanguage.FirstOrDefault(x => x.language == language).urlname;
        //        }
        //        else
        //        {
        //            return "c";
        //        }
        //    }
        //    return null;
        //}
        public static string GetLanguageName(int language, int lc = -1)
        {
            if (lc == -1)
            {
                lc = LangConfigHelper;
            }
            if (lc == 0)
            {
                Language lang = (Language)language;
                return lang.ToString();
            }
            else
            {
                LanguageEU lang = (LanguageEU)language;
                return lang.ToString();
            }
        }
    }
}
