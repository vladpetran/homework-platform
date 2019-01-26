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
    public class TeacherRepository
    {

        //List of all teachers
        public List<Teacher> GetAllTeachers()
        {
            var teacherList = new List<Teacher>();
            string cs = ConfigurationManager.ConnectionStrings["TeamB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open(); //Deschidere conexiune
                SqlCommand command = new SqlCommand();
                command.CommandText = "spGetTeachers";
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = connection;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var teacher = new Teacher
                        {
                            TeacherID = Convert.ToInt32(reader["TeacherID"]),
                            UserName = reader["UserName"].ToString(),
                            Password = reader["Password"].ToString(),
                            Email = reader["Email"].ToString(),
                            RoleID = reader["RoleID"].ToString(),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            HomeAddress = reader["HomeAddress"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString(),
                        };
                        teacherList.Add(teacher);
                    }
                }
            }
            return teacherList;
        }


        //Add  Teacher
        public void AddTeacher(Teacher newTeacher)
        {
            string cs = ConfigurationManager.ConnectionStrings["TeamB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cs))
            {
                if (newTeacher.Password != null)
                {
                    newTeacher.Password = EncodePasswordToBase64(newTeacher.Password);
                }
                connection.Open(); //Deschidere conexiune
                SqlCommand command = new SqlCommand();
                command.CommandText = "spAddAndVerifyTeacher";
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = connection;
                command.Parameters.Add(new SqlParameter("@pUserName", SqlDbType.NVarChar) { Value = newTeacher.UserName });
                command.Parameters.Add(new SqlParameter("@pPassword", SqlDbType.NVarChar) { Value = newTeacher.Password });
                command.Parameters.Add(new SqlParameter("@pEmail", SqlDbType.NVarChar) { Value = newTeacher.Email });
                command.Parameters.Add(new SqlParameter("@pFirstName", SqlDbType.NVarChar) { Value = newTeacher.FirstName });
                command.Parameters.Add(new SqlParameter("@pLastName", SqlDbType.NVarChar) { Value = newTeacher.LastName });
                command.Parameters.Add(new SqlParameter("@pHomeAddress", SqlDbType.NVarChar) { Value = newTeacher.HomeAddress });
                command.Parameters.Add(new SqlParameter("@pPhoneNumber", SqlDbType.NVarChar) { Value = newTeacher.PhoneNumber });
                command.Connection = connection;
                command.ExecuteNonQuery();
            }

        }
        public static string EncodePasswordToBase64(string password)
        {


            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] inArray = HashAlgorithm.Create("SHA1").ComputeHash(bytes);
            return Convert.ToBase64String(inArray);

        }


        //Update Teacher
        public void UpdateTeacher(Teacher teacher)
        {
            string cs = ConfigurationManager.ConnectionStrings["TeamB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open(); //Deschidere conexiune
                SqlCommand command = new SqlCommand();
                command.CommandText = "spUpdateTeacher";
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = connection;
                command.Parameters.Add(new SqlParameter("@pID", teacher.TeacherID));
                if (teacher.Password != "" || teacher.Password != null)
                {
                    command.Parameters.Add(new SqlParameter("@pPassword", teacher.Password));
                }
                command.Parameters.Add(new SqlParameter("@pEmail", teacher.Email));
                command.Parameters.Add(new SqlParameter("@pFirstName", teacher.FirstName));
                command.Parameters.Add(new SqlParameter("@pLastName", teacher.LastName));
                command.Parameters.Add(new SqlParameter("@pHomeAddress", teacher.HomeAddress));
                command.Parameters.Add(new SqlParameter("@pPhoneNumber", teacher.PhoneNumber));
                command.Connection = connection;
                command.ExecuteNonQuery();
            }

        }


        //Delete Teacher
        public void DeleteTeacher(int id)
        {
            string cs = ConfigurationManager.ConnectionStrings["TeamB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open(); //Deschidere conexiune
                SqlCommand command = new SqlCommand();
                command.CommandText = "spDeleteTeacher";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@pID", id);
                if (command.ExecuteNonQuery() > 0)
                    Console.WriteLine("Succeessfully deleted");

                else
                    Console.WriteLine("Row not deleted");
                connection.Close();
            }
        }


        //GET TEACHER BY ID
        public Teacher GetTeacherByID(int id)
        {
            string cs = ConfigurationManager.ConnectionStrings["TeamB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "spGetTeacherByID";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID", id);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var teacherId = new Teacher
                        {
                            UserName = reader["UserName"].ToString(),
                            Password = reader["Password"].ToString(),
                            Email = reader["Email"].ToString(),
                            RoleID = reader["RoleID"].ToString(),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            HomeAddress = reader["HomeAddress"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString(),
                        };
                        if (teacherId != null)
                            return teacherId;
                    }
                }

            }
            return null;
        }

        public Teacher GetTeacherByTeacherName(string teacherName)
        {
            List<Teacher> teachers = new List<Teacher>();
            Teacher teacher;
            string cs = ConfigurationManager.ConnectionStrings["TeamB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open(); //Deschidere conexiune
                SqlCommand command = new SqlCommand();
                command.CommandText = "spGetTeacherByUserName";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Name", teacherName);
                command.Connection = connection;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        teacher = new Teacher()
                        {
                            TeacherID = reader.GetInt32(reader.GetOrdinal("TeacherID")),
                            Email = reader["Email"].ToString(),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            HomeAddress = reader["HomeAddress"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString()

                        };
                        teachers.Add(teacher);
                    }

                }
            }
            return teachers.FirstOrDefault();
        }

    }
}
