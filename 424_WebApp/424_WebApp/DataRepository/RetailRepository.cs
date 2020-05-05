using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Dapper;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

using _424_WebApp.Models;
using System.Text.RegularExpressions;
using Microsoft.Owin.Security.Provider;

namespace _424_WebApp.DataRepository
{
    public class RetailRepository
    {
        private string connectionStrName = "RSE";
        private string connection;

        //constructor
        public RetailRepository()
        {
            connection = ConfigurationManager.ConnectionStrings[connectionStrName].ToString();
        }

        public List<Product> Search(string query)
        {
            using (IDbConnection db = new SqlConnection(connection))
            {
                List<Product> search = db.Query<Product>("SearchProcedure", new { query }, commandType: CommandType.StoredProcedure).ToList();

                return search;
            }
        }

        public ProductVM GetProductCategoryAssignments(int ProductID)
        {
            ProductVM productVM = new ProductVM();

            Product product = GetProductInfo(ProductID);

            productVM.ItemName = product.ItemName;
            productVM.ImageName = product.ImageName;
            productVM.ProductID = ProductID;

            using (IDbConnection db = new SqlConnection(connection))
            {
                string sql = @"SELECT c.CategoryID, c.CatLabel, iif(pc.ProductID is not null,'checked', '') as IsChecked
                FROM Category c
                LEFT JOIN ProductCategory pc
                ON c.CategoryID = pc.CategoryID 
                AND pc.ProductID = @ProductID";

                productVM.Categories = db.Query<CategoryVM>(sql, new { ProductID }).ToList();
            }

            return productVM;
        }

        public void AddRemoveProductCategoryAssignments(AjaxResponseVM ar)
        {
            string sql;

            if(ar.Action == "insert")
            {
                sql = @"INSERT INTO ProductCategory (ProductID, CategoryID) VALUES (@productID, @categoryID)";
            }
            else
            {
                sql = @"DELETE FROM ProductCategory WHERE ProductID = @productID AND CategoryID = @categoryID";
            }

            using (IDbConnection db = new SqlConnection(connection))
            {
                db.Execute(sql, new { productID = ar.ProductID, categoryID = ar.CategoryID });
            }
        }

        public Product GetProductInfo(int ProductID)
        {
            using (IDbConnection db = new SqlConnection(connection))
            {
                string sql = @"SELECT ProductID, ItemName, ImageName FROM Product WHERE ProductID = @ProductID";

                Product product = db.Query<Product>(sql, new { ProductID }).SingleOrDefault();

                return product;
            }
        }

    }
}