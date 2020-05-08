using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

using _424_WebApp.DataRepository;
using _424_WebApp.Models;
using System.Web.UI.WebControls;

namespace _424_WebApp.Controllers
{
    public class MusicSearchApiController : Controller
    {
        // search music method
        public JsonResult SearchMusic(string id)
        {
            // create music repo obj
            MusicRepository musicRepo = new MusicRepository();

            // return json
            return Json(musicRepo.SearchMusic(id), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConsumeApi()
        {
            //URL of API (use yours from part 1 on Yorktown)
            string url = "https://yorktown.cbe.wwu.edu/students/182/mis424Assignments/MusicSearchApi/searchMusic/love";

            // local test var
            //string url = "http://localhost:44356/MusicSearchApi/SearchMusic/love";

            List<MusicItemModel> model = null;
            var client = new HttpClient();
            var task = client.GetAsync(url)
              .ContinueWith((taskwithresponse) =>
              {
                  var response = taskwithresponse.Result;
                  var jsonString = response.Content.ReadAsStringAsync();
                  jsonString.Wait();

                  JavaScriptSerializer js = new JavaScriptSerializer();

                  model = js.Deserialize<List<MusicItemModel>>(jsonString.Result);

              });
            task.Wait(); 
            return View(model);
        }

        public ActionResult ConsumeApi2(string query)
        {

            if (query == null)
            {
                return View();
            }
            //URL of API (use yours from part 1 on Yorktown)
            string url = "https://yorktown.cbe.wwu.edu/students/182/mis424Assignments/MusicSearchApi/searchMusic/" + query;

            List<MusicItemModel> model = null;
            var client = new HttpClient();
            var task = client.GetAsync(url)
              .ContinueWith((taskwithresponse) =>
              {
                  var response = taskwithresponse.Result;
                  var jsonString = response.Content.ReadAsStringAsync();
                  jsonString.Wait();

                  JavaScriptSerializer js = new JavaScriptSerializer();

                  model = js.Deserialize<List<MusicItemModel>>(jsonString.Result);

              });
            task.Wait();

            ViewBag.count = model.Count;
            ViewBag.q = query;

            return View(model);
        }
    }
}