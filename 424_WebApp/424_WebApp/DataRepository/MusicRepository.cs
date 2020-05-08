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
using System.Runtime.CompilerServices;

namespace _424_WebApp.DataRepository
{
    public class MusicRepository
    {
        private string connectionStrName = "MusicStore";
        private string connection;

        // constructor
        public MusicRepository()
        {
            connection = ConfigurationManager.ConnectionStrings[connectionStrName].ToString();
        }

        public List<MusicItemModel> SearchMusic(string query)
        {
            using (IDbConnection db = new SqlConnection(connection))
            {
                string sql = "SELECT * FROM tblDescription d WHERE d.Title LIKE @query OR d.Artists LIKE @query";

                List<MusicItemModel> search = db.Query<MusicItemModel>(sql, new { query = "%"+query+"%"}).ToList();

                return search;
            }
        }
    }
}