using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ServiceModel;
using System.Xml;
using System.ServiceModel.Syndication;

using _424_WebApp.Models;

namespace _424_WebApp.Controllers
{
    public class RssController : Controller
    {
        // GET: Rss
        public ActionResult Index()
        {
            string rssFeedUrl = "https://www.seattletimes.com/feed/";

            var reader = XmlReader.Create(rssFeedUrl);
            var feed = SyndicationFeed.Load(reader);

            List<RssModel> rssList = new List<RssModel>();

            //Loop through all items in the SyndicationFeed
            foreach (var i in feed.Items)
            {
                RssModel article = new RssModel();
                article.Title = i.Title.Text;
                article.PubDate = i.PublishDate.ToString();
                article.Link = i.Links[0].Uri.OriginalString;
                article.Description = i.Summary.Text;
                rssList.Add(article);
            }

            ViewBag.count = feed.Items.Count();

            ViewBag.feed = rssFeedUrl;

            return View(rssList);
        }
    }
}