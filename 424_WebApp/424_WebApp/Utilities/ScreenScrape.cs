using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace _424_WebApp.Utilities
{
    // Retrives data from URL
    public static class ScreenScrape
    {
        public static string GetHTML(string URL)
        {
            HttpWebRequest webRequest = WebRequest.Create(URL) as HttpWebRequest;

            webRequest.Timeout = 3000;
            webRequest.UserAgent = "Greetings from MIS 424 class! cooperz@wwu.edu";
            webRequest.Accept = "*/*";
            webRequest.AutomaticDecompression = DecompressionMethods.GZip;

            WebResponse webResponse = webRequest.GetResponse();

            string html;

            using (StreamReader sr = new StreamReader(webResponse.GetResponseStream()))
            {
                html = sr.ReadToEnd();
            }
            return html;
        }
    }
}