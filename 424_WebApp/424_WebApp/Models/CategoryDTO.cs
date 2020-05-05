using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _424_WebApp.Models
{
    public class CategoryDTO
    {
        [Key]
        public int CategoryID { get; set; }
        public string CatLabel { get; set; }
        public int Count { get; set; }
    }
}