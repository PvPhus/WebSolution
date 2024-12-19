using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace WebSolution.WebHelper
{
    public class HtmlProcess
    {
        public static string ExtractImage(string htmlcontent, int minImgSize)
        {
            if (!String.IsNullOrEmpty(htmlcontent))
            {
                try
                {
                    var doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(htmlcontent);

                    var htmlNode = doc.DocumentNode.SelectNodes("//img[@src]");

                    foreach (var item in htmlNode)
                    {
                        string url = item.Attributes["src"].Value;
                        var wbRequest = (HttpWebRequest)WebRequest.Create(url);
                        wbRequest.UserAgent =
                            "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.2.8) Gecko/20100722 Firefox/3.6.8";
                        wbRequest.KeepAlive = true;
                        wbRequest.Timeout = 60000;

                        var resp = (HttpWebResponse)wbRequest.GetResponse();
                        if (resp.ContentLength > minImgSize)
                            return url;
                    }

                }
                catch (Exception e)
                {
                    return "";
                }
            }
            return "";
        }

        public static string RepaireLink(string source, string sourceUrl)
        {
            const string pattern = @"(src|url|href|action)[= ]('|\"")[\d\w\/:#@%;$\(\)~_\?\+\-=\\\.&]*('|\"")";
            MatchCollection matchCollection = Regex.Matches(source, pattern, RegexOptions.IgnoreCase);

            var newLinkCollection = new List<string>();
            var removedLink = new List<string>();

            foreach (var item in matchCollection)
            {
                string temp = item.ToString();
                while (temp.IndexOf("\"") != -1)
                {
                    temp = temp.Replace("\"", "'");
                }

                if (temp.IndexOf("'/'") != -1)
                {
                    continue;
                }

                string prefix = temp.Substring(0, temp.IndexOf("'"));
                string mylink = temp.Substring(temp.IndexOf("'") + 1, temp.Length - temp.IndexOf("'") - 2).Trim();

                if (!String.IsNullOrEmpty(mylink))
                {
                    if (mylink.IndexOf("http://") == 0)
                    {
                        removedLink.Add(temp);
                        newLinkCollection.Add(temp);
                        continue;
                    }
                    if (mylink.IndexOf("/") == 0)
                    {
                        removedLink.Add(temp);
                        newLinkCollection.Add(prefix + "'" + sourceUrl + mylink + "'");

                    }
                    else
                    {
                        removedLink.Add(mylink);
                        newLinkCollection.Add(prefix + "'" + sourceUrl + "/" + mylink + "'");
                    }
                }
            }
            for (int i = 0; i < removedLink.Count; i++)
                source = source.Replace(removedLink[i], newLinkCollection[i]);
            return source;
        }

        public static HtmlDocument RepaireHtml(HtmlDocument doc, string sourceUrl)
        {
            //var doc = new HtmlAgilityPack.HtmlDocument();
            // doc.LoadHtml(source);

            var link = doc.DocumentNode.SelectNodes("//a[@href]");
            foreach (var element in link)
            {
                HtmlAttribute attr = element.Attributes["href"];
                attr.Value = FixValue(attr.Value, sourceUrl);
            }

            var img = doc.DocumentNode.SelectNodes("//img[@src]");
            foreach (var element in img)
            {
                HtmlAttribute attr = element.Attributes["src"];
                attr.Value = FixValue(attr.Value, sourceUrl);
            }

            var style = doc.DocumentNode.SelectNodes("//link[@href]");
            foreach (var element in style)
            {
                HtmlAttribute attr = element.Attributes["href"];
                attr.Value = FixValue(attr.Value, sourceUrl);
            }

            var script = doc.DocumentNode.SelectNodes("//script[@src]");
            foreach (var element in script)
            {
                HtmlAttribute attr = element.Attributes["src"];
                attr.Value = FixValue(attr.Value, sourceUrl);
            }
            // source = doc.DocumentNode.InnerHtml;

            return doc;
        }

        public static string FixValue(string value, string sourceUrl)
        {
            value = value.Trim();
            if (value == "/")
            {
                value = sourceUrl;
                return value;
            }
            if (value.IndexOf("http://") == 0)
            {
                return value;
            }
            if (value.IndexOf("/") == 0)
            {
                value = sourceUrl + value;
            }
            else
            {
                value = sourceUrl + "/" + value;
            }
            return value;
        }

        public static string SourceUrl(string url)
        {
            try
            {
                if (url.IndexOf("http://") == 0)
                {
                    url = url.Replace("http://", "");
                }
                url += "/";
                url = url.Substring(0, url.IndexOf("/"));
                return "http://" + url;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string ReplaceTagsStrongEm(string source)
        {
            try
            {
                string result = "";
                // Remove tags <strong>
                result = Regex.Replace(result, @"<( )*strong([^>])*>", "\r\r", RegexOptions.IgnoreCase);
                // Remove tags <em>
                result = Regex.Replace(result,
                                                                      @"<( )*em([^>])*>", "\r\r",
                                                                      RegexOptions.
                                                                          IgnoreCase);
                return result;
            }
            catch (Exception)
            {
                return source;
            }
        }

        public static string StripHtml(string source)
        {
            try
            {
                string result;
                result = source.Replace("\r", " ");
                // Replace line breaks with space because browsers inserts space
                result = result.Replace("\n", " ");
                // Remove step-formatting
                result = result.Replace("\t", string.Empty);
                // Remove repeating spaces because browsers ignore them
                result = Regex.Replace(result, @"( )+", " ");
                // Remove the header (prepare first by clearing attributes)
                result = Regex.Replace(result,
                         @"<( )*head([^>])*>", "<head>",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"(<( )*(/)( )*head( )*>)", "</head>",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         "(<head>).*(</head>)", string.Empty,
                         RegexOptions.IgnoreCase);
                // remove all scripts (prepare first by clearing attributes)
                result = Regex.Replace(result,
                         @"<( )*script([^>])*>", "<script>",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"(<( )*(/)( )*script( )*>)", "</script>",
                         RegexOptions.IgnoreCase);
                //result = Regex.Replace(result, 
                //         @"(<script>)([^(<script>\.</script>)])*(</script>)",
                //         string.Empty, 
                //         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"(<script>).*(</script>)", string.Empty,
                         RegexOptions.IgnoreCase);
                // remove all styles (prepare first by clearing attributes)
                result = Regex.Replace(result,
                         @"<( )*style([^>])*>", "<style>",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"(<( )*(/)( )*style( )*>)", "</style>",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         "(<style>).*(</style>)", string.Empty,
                         RegexOptions.IgnoreCase);
                // insert tabs in spaces of <td> tags
                result = Regex.Replace(result,
                         @"<( )*td([^>])*>", "\t",
                         RegexOptions.IgnoreCase);
                // insert line breaks in places of <BR> and <LI> tags
                result = Regex.Replace(result,
                         @"<( )*br( )*>", "\r",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"<( )*li( )*>", "\r",
                         RegexOptions.IgnoreCase);
                // insert line paragraphs (double line breaks) in place
                // if <P>, <DIV> and <TR> tags
                result = Regex.Replace(result,
                         @"<( )*div([^>])*>", "\r\r",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"<( )*tr([^>])*>", "\r\r",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"<( )*p([^>])*>", "\r\r",
                         RegexOptions.IgnoreCase);
                // Remove remaining tags like <a>, links, images,
                // comments etc - anything thats enclosed inside < >
                result = Regex.Replace(result,
                         @"<[^>]*>", string.Empty,
                         RegexOptions.IgnoreCase);
                // replace special characters:
                result = Regex.Replace(result,
                         @" ", " ",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&bull;", " * ",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&lsaquo;", "<",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&rsaquo;", ">",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&trade;", "(tm)",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&frasl;", "/",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&lt;", "<",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&gt;", ">",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&copy;", "(c)",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&reg;", "(r)",
                         RegexOptions.IgnoreCase);
                // Remove all others. More can be added, see
                // http://hotwired.lycos.com/webmonkey/reference/special_characters/
                result = Regex.Replace(result,
                         @"&(.{2,6});", string.Empty,
                         RegexOptions.IgnoreCase);
                // for testing

                //Regex.Replace(result, 

                //       this.txtRegex.Text,string.Empty, 

                //       RegexOptions.IgnoreCase);


                // make line breaking consistent

                result = result.Replace("\n", "\r");

                // Remove extra line breaks and tabs:

                // replace over 2 breaks with 2 and over 4 tabs with 4. 

                // Prepare first to remove any whitespaces inbetween

                // the escaped characters and remove redundant tabs inbetween linebreaks

                result = Regex.Replace(result,
                         "(\r)( )+(\r)", "\r\r",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         "(\t)( )+(\t)", "\t\t",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         "(\t)( )+(\r)", "\t\r",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         "(\r)( )+(\t)", "\r\t",
                         RegexOptions.IgnoreCase);
                // Remove redundant tabs

                result = Regex.Replace(result,
                         "(\r)(\t)+(\r)", "\r\r",
                         RegexOptions.IgnoreCase);
                // Remove multible tabs followind a linebreak with just one tab

                result = Regex.Replace(result,
                         "(\r)(\t)+", "\r\t",
                         RegexOptions.IgnoreCase);
                // Initial replacement target string for linebreaks

                string breaks = "\r\r\r";
                // Initial replacement target string for tabs

                string tabs = "\t\t\t\t\t";
                for (int index = 0; index < result.Length; index++)
                {
                    result = result.Replace(breaks, "\r\r");
                    result = result.Replace(tabs, "\t\t\t\t");
                    breaks = breaks + "\r";
                    tabs = tabs + "\t";
                }

                // Thats it.

                return result;

            }
            catch
            {
                return source;
            }
        }

        public static string StripScript(string source)
        {
            string s = Regex.Replace(source, "<script.*?</script>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            return s;
        }

        public static string FillterHtmlTag(string html, string removeTags)
        {
            /// removeTags format: script|embed|object|frameset|frame|iframe|meta|link|style
            string ret = Regex.Replace(html, @"</?(?i:" + removeTags + ")(.|\n)*?>", "");
            return ret;
        }

        public static bool CheckScriptType(string script, string type)
        {
            return true;
        }

        public static string ProperAttach(string attach, string type)
        {
            if (!String.IsNullOrEmpty(attach))
            {
                switch (type.ToLower())
                {
                    case "link":
                        return "<a href = '" + attach + "' target = '_blank' />";
                    case "image":
                        return "<img src='" + attach + "' style = 'max-width:550px; max-height:400px' />";
                    default:
                        return "<div style = 'max-width:550px; max-height:400px'>" + attach + "</div>";
                }
            }
            return "";

        }

    }
    public static class CleanHtml
    {
        public static bool IsHtmlFragment(string value)
        {
            return Regex.IsMatch(value, @"</?(p|div)>");
        }

        /// <summary>
        /// Remove tags from a html string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string CleanHtmlFull(string value)
        {
            if (value != null)
            {
                value = CleanHtmlComments(value);
                value = CleanHtmlBehaviour(value);
                //value = CleanHtmlHref(value);
                value = HtmlAgilityPackCleanup(value);
            }
            return value;
        }

        public static string GetTextFromHTML(string value)
        {
            if (value != null)
            {
                value = Regex.Replace(value, @"</[^>]+?>", " ");
                value = Regex.Replace(value, @"<[^>]+?>", "");
            }
            return value;
        }

        public static string CleanHtmlHref(string value)
        {
            return Regex.Replace(value, "(<[a|A][^>]*>|)", "");
        }

        /// <summary>
        /// Clean script and styles html tags and content
        /// </summary>
        /// <returns></returns>
        public static string CleanHtmlBehaviour(string value)
        {
            value = Regex.Replace(value, "(<style.+?</style>)|(<script.+?</script>)", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            value = new Regex("style=\"[^\"]*\"").Replace(value, "");
            value = new Regex("(?<=class=\")([^\"]*)\\babc\\w*\\b([^\"]*)(?=\")").Replace(value, "$1$2");
            return value;
        }

        /// <summary>
        /// Replace the html commens (also html ifs of msword).
        /// </summary>
        public static string CleanHtmlComments(string value)
        {
            //Remove disallowed html tags.
            value = Regex.Replace(value, "<!--.+?-->", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);

            return value;
        }

        /// <summary>
        /// Adds rel=nofollow to html anchors
        /// </summary>
        public static string HtmlLinkAddNoFollow(string value)
        {
            return Regex.Replace(value, "<a[^>]+href=\"?'?(?!#[\\w-]+)([^'\">]+)\"?'?[^>]*>(.*?)</a>", "<a href=\"$1\" rel=\"nofollow\" target=\"_blank\">$2</a>", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }
        private static string HtmlAgilityPackCleanup(string html)
        {
            try
            {
                HtmlDocument doc = new HtmlDocument();
                doc.OptionWriteEmptyNodes = true;
                doc.LoadHtml(html);

                var bodyNodes = doc.DocumentNode.SelectNodes("//body");
                if (bodyNodes != null)
                {
                    foreach (HtmlNode nodeBody in bodyNodes)
                    {
                        nodeBody.Attributes.Remove("style");
                    }
                }

                var scriptNodes = doc.DocumentNode.SelectNodes("//script");
                if (scriptNodes != null)
                {
                    foreach (HtmlNode nodeScript in scriptNodes)
                    {
                        nodeScript.Remove();
                    }
                }

                var linkNodes = doc.DocumentNode.SelectNodes("//link");
                if (linkNodes != null)
                {
                    foreach (HtmlNode nodeLink in linkNodes)
                    {
                        nodeLink.Remove();
                    }
                }

                var xmlNodes = doc.DocumentNode.SelectNodes("//xml");
                if (xmlNodes != null)
                {
                    foreach (HtmlNode nodeXml in xmlNodes)
                    {
                        nodeXml.Remove();
                    }
                }

                var styleNodes = doc.DocumentNode.SelectNodes("//style");
                if (styleNodes != null)
                {
                    foreach (HtmlNode nodeStyle in styleNodes)
                    {
                        nodeStyle.Remove();
                    }
                }

                foreach (var eachNode in doc.DocumentNode.SelectNodes("//a"))
                {
                    if (eachNode.HasAttributes)
                    {
                        int attCount = eachNode.Attributes.Count;
                        for (int i = 0; i < attCount; i++)
                        {
                            if (eachNode.Attributes[i].Name.ToLower() == "ref")
                            {
                                if (eachNode.Attributes[i].Value != null)
                                {
                                    string host = eachNode.Attributes[i].Value;
                                    try
                                    {
                                        if (!string.IsNullOrEmpty(host))
                                            if (host.IndexOf("../detail/") > -1 || host.IndexOf("../download/") > -1 || host.IndexOf("webtech360.com") > -1 || host.IndexOf("afamilytoday.com") > -1)
                                            {

                                            }
                                            else
                                            {
                                                eachNode.Attributes.RemoveAt(i);
                                                attCount--;
                                            }
                                    }
                                    catch
                                    {
                                        eachNode.Attributes.RemoveAt(i);
                                        attCount--;
                                    }
                                }
                            }
                            else if (eachNode.Attributes[i].Name.ToLower() == "href")
                            {
                                if (eachNode.Attributes[i].Value != null)
                                {
                                    string host = eachNode.Attributes[i].Value;
                                    try
                                    {
                                        if (!string.IsNullOrEmpty(host))
                                            if (host.IndexOf("../detail/") > -1 || host.IndexOf("../download/") > -1 || host.IndexOf("webtech360.com") > -1 || host.IndexOf("afamilytoday.com") > -1)
                                            {

                                            }
                                            else
                                            {
                                                eachNode.Attributes[i].Value = "#";
                                                attCount--;
                                            }
                                    }
                                    catch
                                    {
                                        eachNode.Attributes.RemoveAt(i);
                                        attCount--;
                                    }
                                }
                            }
                        }
                    }
                }

                var result = doc.DocumentNode.OuterHtml;
                return result;
            }
            catch
            { }
            return html;
        }
    }
}
