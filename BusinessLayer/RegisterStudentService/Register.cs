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

namespace BusinessLayer.RegisterStudentService
{
    public class Register
    {

        public int userID { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public Role roleID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string homeAddress { get; set; }
        public string phoneNumber { get; set; }
        public bool EmailConfirmed { get; set; }

        public void Add(Student newStudent)
        {
            string cs = ConfigurationManager.ConnectionStrings["TeamB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cs))
            {
                try
                {
                    newStudent.Password = EncodePasswordToBase64(newStudent.Password);
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.CommandText = "spAddAndVerifyStudent";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;
                    command.Parameters.Add(new SqlParameter("@pUserName", SqlDbType.NVarChar) { Value = newStudent.UserName });
                    command.Parameters.Add(new SqlParameter("@pPassword", SqlDbType.NVarChar) { Value = newStudent.Password });
                    command.Parameters.Add(new SqlParameter("@pEmail", SqlDbType.NVarChar) { Value = newStudent.Email });
                    command.Parameters.Add(new SqlParameter("@pFirstName", SqlDbType.NVarChar) { Value = newStudent.FirstName });
                    command.Parameters.Add(new SqlParameter("@pLastName", SqlDbType.NVarChar) { Value = newStudent.LastName });
                    command.Parameters.Add(new SqlParameter("@pHomeAddress", SqlDbType.NVarChar) { Value = newStudent.HomeAddress });
                    command.Parameters.Add(new SqlParameter("@pPhoneNumber", SqlDbType.NVarChar) { Value = newStudent.PhoneNumber });
                    command.Connection = connection;
                    command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }
        public static string EncodePasswordToBase64(string password)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] inArray = HashAlgorithm.Create("SHA1").ComputeHash(bytes);
            return Convert.ToBase64String(inArray);
        }
    }
}

