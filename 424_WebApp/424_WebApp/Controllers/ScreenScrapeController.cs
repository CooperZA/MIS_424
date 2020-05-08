using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Text.RegularExpressions;
using System.Text;
using _424_WebApp.Utilities;

namespace _424_WebApp.Controllers
{
    public class ScreenScrapeController : Controller
    {
        // GET: ScreenScrape
        [ValidateInput(false)]
        public ActionResult Index(string targetUrl, string regExPattern, string viewHtml)
        {
            if (regExPattern == null)
                ViewBag.pattern = "(?<=mailto:).*?.edu";
            else
                ViewBag.pattern = regExPattern;

            if (viewHtml != null)
                ViewBag.viewHtml = "checked";

            if (targetUrl == null)
            {
                ViewBag.targetUrl = "https://cbe.wwu.edu/faculty-staff";
                return View();
            }

            ViewBag.targetUrl = targetUrl;
            //scrape data
            string html = ScreenScrape.GetHTML(targetUrl);

            if (viewHtml != null)
            {
                ViewBag.results = html;
                return View();
            }
            //use regular expressions to parse out interesting bits
            Regex objRegEx = new Regex(regExPattern, (RegexOptions.Singleline | RegexOptions.IgnoreCase));
            MatchCollection objMatchCollection;

            //stringBuilder class is for efficient string concatenation
            StringBuilder results = new StringBuilder();

            objMatchCollection = objRegEx.Matches(html);
            ViewBag.count = objMatchCollection.Count + " matches.";
            foreach (Match objMatch in objMatchCollection)
            {
                //add a break & carriage return at end of each match
                string match = objMatch.Value + "<br>\n";

                //May need to clean up results using .Replace() or regular expressions:
                //match = Regex.Replace(match, "some text", "", RegexOptions.IgnoreCase);
                //Regular expressions may use look ahead "?=" and look behind "?<=" 
                //to specify text that is part of the search but not returned
                //as part of the match. 
                results.Append(match);
            }
            ViewBag.results = results.ToString();
            return View();
        }

        public ActionResult WwuNews()
        {
            //if (regExPattern == null)
            //    ViewBag.pattern = "(?<=mailto:).*?.edu";
            //else
            //    ViewBag.pattern = regExPattern;

            //if (viewHtml != null)
            //    ViewBag.viewHtml = "checked";

            //if (targetUrl == null)
            //{
            //    ViewBag.targetUrl = "https://cbe.wwu.edu/faculty-staff";
            //    return View();
            //}

            //ViewBag.targetUrl = targetUrl;

            // hard coded variables
            string targetUrl = "https://westerntoday.wwu.edu/news";

            string regExPattern = "<h2 class=\"field-content helper-clear-float\"><a href=\"/news.*?</h2>";

            //scrape data
            string html = ScreenScrape.GetHTML(targetUrl);

            //if (viewHtml != null)
            //{
            //    ViewBag.results = html;
            //    return View();
            //}
            //use regular expressions to parse out interesting bits
            Regex objRegEx = new Regex(regExPattern, (RegexOptions.Singleline | RegexOptions.IgnoreCase));

            MatchCollection objMatchCollection;

            //stringBuilder class is for efficient string concatenation
            StringBuilder results = new StringBuilder();

            objMatchCollection = objRegEx.Matches(html);
            ViewBag.count = objMatchCollection.Count + " matches.";
            foreach (Match objMatch in objMatchCollection)
            {
                //add a break & carriage return at end of each match
                string match = objMatch.Value + "<br>\n";

                //May need to clean up results using .Replace() or regular expressions:
                match = Regex.Replace(match, "/news", targetUrl, RegexOptions.IgnoreCase);
                //Regular expressions may use look ahead "?=" and look behind "?<=" 
                //to specify text that is part of the search but not returned
                //as part of the match. 
                results.Append(match);
            }
            ViewBag.results = results.ToString();
            return View();
        }
    }
}