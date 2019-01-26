using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace DataAccessLayer.Repository
{
    public class UserRepository
    {

        public List<Tuple<string, string, string, int>> GetUserByUserName(string UserName)
        {
            var userList = new List<Tuple<string, string, string, int>> { };
            string cs = ConfigurationManager.ConnectionStrings["TeamB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "spGetUserByName";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Name", UserName);
                command.Connection = connection;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        userList.Add(Tuple.Create(reader["UserName"].ToString(),
                            Convert.ToString(reader["Password"]),
                            reader["RoleID"].ToString(),
                            Convert.ToInt32(reader["UserID"])));

                        if (userList != null)
                        {
                            return userList;
                        }
                    }


                }
                return null;
            }

        }

        public bool GetConfirm(string UserName)
        {
            bool UserConfirm = false;
            string cs = ConfigurationManager.ConnectionStrings["TeamB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "spGetConfirm";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Name", UserName);
                command.Connection = connection;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        UserConfirm = (bool)reader["EmailConfirm"];
                    }


                }
                return UserConfirm;
            }

        }

        public int ResetUserPassword(string email, string password)
        {
            string cs = ConfigurationManager.ConnectionStrings["TeamB"].ConnectionString;
            int rows = -1;
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();
                try
                {
                    string password2 = EncodePasswordToBase64(password);
                    SqlCommand command = new SqlCommand();
                    command.CommandText = "spResetUserPass";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@pEmail", email);
                    command.Parameters.AddWithValue("@pPassword", password2);
                    command.Connection = connection;
                    rows = command.ExecuteNonQuery();
                }
                catch (SqlException )
                {
                    return rows;
                }
            }
            return rows;
        }

        public static string EncodePasswordToBase64(string password)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] inArray = HashAlgorithm.Create("SHA1").ComputeHash(bytes);
            return Convert.ToBase64String(inArray);
        }
    }
}