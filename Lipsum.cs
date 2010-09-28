using System;
using System.IO;
using System.Net;

namespace lipsum4net
{
    public class Lipsum
    {
        public static string GetWords(int count)
        {
            return Get("words", count);
        }

        public static string GetParagraphs(int count)
        {
            return Get("paragraphs", count);
        }

        public static string GetLists(int count)
        {
            return Get("lists", count);
        }


        private static string Get(string what, int amount)
        {
            WebRequest request = WebRequest.Create("http://www.lipsum.com/feed/html");
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            using (var writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(String.Format("what={0}&amount={0}", what, amount));
            }

            var pageHtml = "";

            using (var response = (HttpWebResponse)request.GetResponse())
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                pageHtml = reader.ReadToEnd();
            }

            var startTag = @"<div id=""lipsum"">";
            var startPos = pageHtml.IndexOf(startTag) + startTag.Length + 1;
            var endPos = pageHtml.IndexOf("</div>", startPos);

            var html = pageHtml.Substring(startPos, endPos - startPos);
            return html;
        }
        
    }
}
