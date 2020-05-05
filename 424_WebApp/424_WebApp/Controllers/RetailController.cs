using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.Helpers;
using _424_WebApp.Models;
using _424_WebApp.DataRepository;
using System.Drawing.Printing;

namespace _424_WebApp.Controllers
{
    [Authorize (Users = "admin@wwu.edu")]
    public class RetailController : Controller
    {
        private RetailStoreEntities db = new RetailStoreEntities();

        // GET: Index
        [AllowAnonymous]
        public ActionResult Index() 
        {
            // pull 6 random records from db
            return View(db.Products.OrderBy(x => Guid.NewGuid()).Take(6));
        }

        [AllowAnonymous]
        public ActionResult Detail(int id)
        {
            return View(db.Products.Find(id));
        }

        [AllowAnonymous]
        public ActionResult _leftMenu()
        {
            var categories = (from c in db.Categories
                              join pc in db.ProductCategories
                                on c.CategoryID equals pc.CategoryID
                                into categorygroup
                              select new CategoryDTO
                              {
                                  CategoryID = c.CategoryID,
                                  CatLabel = c.CatLabel,
                                  Count = categorygroup.Count()
                              }).ToList();

            return View(categories);
        }

        // Get: Retail Category
        [AllowAnonymous]
        public ActionResult Category(int id)
        {
            // LINQ statement
            var products = (from p in db.Products
                           join pc in db.ProductCategories
                           on p.ProductID equals pc.ProductID
                           where pc.CategoryID == id
                           select p).ToList();

            //viewbag message with item count
            ViewBag.message = "There are " + products.Count() + "items.";

            return View("index", products);
        }

        // Retail Search
        [AllowAnonymous]
        public ActionResult Search(string query)
        {
            RetailRepository retailRepo = new RetailRepository();

            List<Product> productList = retailRepo.Search(query);
            ViewBag.message = productList.Count() + " products match '" + query + "'";
            return View(productList);
        }

        // Ajax search
        [AllowAnonymous]
        public ActionResult AjaxSearch()
        {
            return View();
        }

        // Ajax search handler
        [AllowAnonymous]
        public ActionResult AjaxSearchHandler(string id)
        {
            RetailRepository retailRepo = new RetailRepository();

            List<Product> productList = retailRepo.Search(id);

            return Json(productList, JsonRequestBehavior.AllowGet);
        }

        // Get: retail About
        [AllowAnonymous]
        public ActionResult About()
        {
            Random random = new Random();

            int randNum = random.Next(1, (db.Products.Count()) + 1);

            return View(db.Products.Find(randNum));
        }

        /* Admin Pages*/

        // GET: Retail
        public ActionResult Admin()
        {
            return View();
        }

        [HttpGet]
        public ActionResult EditProductCategories(int id = 0)  //productID
        {

            RetailRepository retailRepo = new RetailRepository();

            if (id < 1)
                return RedirectToAction("/admin");

            ProductVM pcVM = retailRepo.GetProductCategoryAssignments(id);
            return View(pcVM);
        }

        // Ajax change prod categories
        [HttpPost]
        public string ProductCategoryAjaxHandler(AjaxResponseVM ajaxResponseVM)
        {
            RetailRepository retailRepo = new RetailRepository();

            retailRepo.AddRemoveProductCategoryAssignments(ajaxResponseVM);

            return " ProductID:" + ajaxResponseVM.ProductID + " CategoryID:" + ajaxResponseVM.CategoryID;
        }

        // POST: Retail, form post and file upload
        [HttpPost]
        public ActionResult Admin(HttpPostedFileBase file)
        {
            // Verify that the user selected a file
            if (file != null && file.ContentLength > 0)
            {
                //allow only certain file types
                string extension = Path.GetExtension(file.FileName).ToLower();
                if (!(extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".gif"))
                {
                    ViewBag.message = "Invalid image extension: " + extension;
                    return View();
                }
                // extract only the filename
                var fileName = Path.GetFileName(file.FileName);
                // store the file inside ~/App_Data/uploads folder
                var path = Path.Combine(Server.MapPath("~/Content/ProductImages"), fileName);


                WebImage img = new WebImage(file.InputStream);

                // check that image is large enough
                if (img.Width < 600 && img.Height < 600)
                {
                    ViewBag.message = "Please use a larger image. Should have a Width or Height >=600px. ";
                    return View();
                }

                if (img.Width > 1000)
                {
                    // declare size array
                    int[] sizes = { 600, 400, 200 };

                    foreach (int size in sizes)
                    {
                        img.Resize(size, size);

                        int dot = path.LastIndexOf(".");

                        string path2 = path.Insert(dot, "." + size);

                        img.Save(path2);
                    }
                }

                ViewBag.message = "Image successfully uploaded: " + fileName;
            }
            // redirect back to the index action to show the form once again
            return View();
        }

        // Delete Image
        [HttpGet]
        public ActionResult DeleteImage(string ImageName)
        {
            string fullPath = Request.MapPath("~/Content/ProductImages/" + ImageName);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            ViewBag.message = "Image successfully deleted: " + ImageName;

            return View("Admin");
        }
    }
}