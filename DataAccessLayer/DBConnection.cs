using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DataAccessLayer
{
    public class DBConnection
    {
        public static readonly DBConnection instance = new DBConnection();
        public SqlConnection cs = new SqlConnection(ConfigurationManager.ConnectionStrings["TeamB"].ConnectionString);

        private DBConnection()
        {
            using (SqlConnection connection = new SqlConnection())
            {
                cs.Open();
            }
        }

        public static DBConnection getInstance(DBConnection cs)
        {
            using (var command = new SqlCommand())
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read()) ;

            }
            return getInstance(cs);
        }

        public SqlConnection GetDBConnection(DBConnection cs)
        {
            return GetDBConnection(cs);
        }
    }
}