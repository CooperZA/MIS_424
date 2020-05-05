using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _424_WebApp.Models
{
    public class CategoryVM
    {
        public int CategoryID { get; set; }
        public string CatLabel { get; set; }
        public string IsChecked { get; set; }
    }
    public class ProductVM
    {
        public ProductVM()
        {
            Categories = new List<CategoryVM>();
        }
        public int ProductID { get; set; }
        public string ItemName { get; set; }
        public string ImageName { get; set; }
        public List<CategoryVM> Categories { get; set; }
    }
}