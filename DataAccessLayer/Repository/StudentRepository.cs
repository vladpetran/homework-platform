using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLayer.Entities;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Security.Cryptography;

namespace DataAccessLayer.Repository
{
    public class StudentRepository
    {

        public StudentInformationDAL GetStudentByStudentName(string studentName)
        {
            List<StudentInformationDAL> students = new List<StudentInformationDAL>();
            StudentInformationDAL student;
            string cs = ConfigurationManager.ConnectionStrings["TeamB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open(); //Deschidere conexiune
                SqlCommand command = new SqlCommand();
                command.CommandText = "spGetStudentByUserName";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Name", studentName);
                command.Connection = connection;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        student = new StudentInformationDAL()
                        {
                            StudentID = reader.GetInt32(reader.GetOrdinal("StudentID")),
                            Email = reader["Email"].ToString(),
                            StudentName = reader["UserName"].ToString()

                        };
                        students.Add(student);
                    }

                }
            }
            return students.FirstOrDefault();
        }


        //get a list of all students
        public List<Student> GetAllStudents()
        {
            var studentList = new List<Student>();
            string cs = ConfigurationManager.ConnectionStrings["TeamB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open(); //Deschidere conexiune
                SqlCommand command = new SqlCommand();
                command.CommandText = "spGetAllStudents";
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = connection;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var student = new Student
                        {
                            StudentID = Convert.ToInt32(reader["StudentID"]),
                            Email = reader["Email"].ToString(),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            HomeAddress = reader["HomeAddress"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString(),
                            //TeacherID = Convert.ToInt32( reader["TeacherID"])

                        };
                        if (!(reader["TeacherID"] is DBNull))
                        {
                            student.TeacherID = Convert.ToInt32(reader["TeacherID"]);
                        }
                        studentList.Add(student);
                    }
                }
            }
            return studentList;
        }

        //GetStudenttsBy teacherID
        public List<Student> GetAllStudentsByTeacherID(int id)
        {
            var studentTeacherList = new List<Student>();
            string cs = ConfigurationManager.ConnectionStrings["TeamB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open(); //Deschidere conexiune
                SqlCommand command = new SqlCommand();
                command.CommandText = "spGetAllStudentsByTeacherId";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID", id);
                command.Connection = connection;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var student = new Student
                        {
                            StudentID = Convert.ToInt32(reader["StudentID"]),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Email = reader["Email"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString()
                        };
                        studentTeacherList.Add(student);
                    }
                }
            }
            return studentTeacherList;
        }

        public void AddStudent(Student newStudent)
        {
            string cs = ConfigurationManager.ConnectionStrings["TeamB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cs))
            {
                try
                {
                    if (newStudent.Password != null)
                    {
                        newStudent.Password = EncodePasswordToBase64(newStudent.Password);
                    }
                    connection.Open(); //Deschidere conexiune
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
                    command.Parameters.Add(new SqlParameter("@pTeacherID", SqlDbType.Int) { Value = newStudent.TeacherID });
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


        public void UpdateStudent(Student student)
        {
            string cs = ConfigurationManager.ConnectionStrings["TeamB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open(); //Deschidere conexiune
                SqlCommand command = new SqlCommand();
                command.CommandText = "spUpdateStudent";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@pID", student.StudentID);
                command.Connection = connection;
                command.Parameters.Add(new SqlParameter("@pUserName", SqlDbType.NVarChar) { Value = student.UserName });
                command.Parameters.Add(new SqlParameter("@pPassword", student.Password));
                command.Parameters.Add(new SqlParameter("@pEmail", SqlDbType.NVarChar) { Value = student.Email });
                command.Parameters.Add(new SqlParameter("@pFirstName", SqlDbType.NVarChar) { Value = student.FirstName });
                command.Parameters.Add(new SqlParameter("@pLastName", SqlDbType.NVarChar) { Value = student.LastName });
                command.Parameters.Add(new SqlParameter("@pHomeAddress", SqlDbType.NVarChar) { Value = student.HomeAddress });
                command.Parameters.Add(new SqlParameter("@pPhoneNumber", SqlDbType.NVarChar) { Value = student.PhoneNumber });
                command.Parameters.Add(new SqlParameter("@pEmailConfirm", true));
                command.Parameters.Add(new SqlParameter("@pTeacherID", SqlDbType.Int) { Value = student.TeacherID });

                command.Connection = connection;
                command.ExecuteNonQuery();
            }

        }


        public void DeleteStudent(int id)
        {
            string cs = ConfigurationManager.ConnectionStrings["TeamB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open(); //Deschidere conexiune
                SqlCommand command = new SqlCommand();
                command.CommandText = "spDeleteStudent";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@pID", id);
                if (command.ExecuteNonQuery() > 0)
                    //return new JavascriptResult { Script = "alert('Successfully registered');" };
                    Console.WriteLine("Succeessfully deleted");

                else
                    Console.WriteLine("Row not deleted");
                connection.Close();
            }
        }

        public Student GetStudentByID(int id)
        {
            string cs = ConfigurationManager.ConnectionStrings["TeamB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open(); //Deschidere conexiune
                SqlCommand command = new SqlCommand();
                command.CommandText = "spGetStudentByID";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID", id);
                command.Connection = connection;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var studentId = new Student
                        {
                            UserName = reader["UserName"].ToString(),
                            Email = reader["Email"].ToString(),
                            RoleID = reader["RoleID"].ToString(),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            HomeAddress = reader["HomeAddress"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString(),
                            StudentID = id,

                        };
                        if (studentId != null)
                            return studentId;

                    }
                }
            }
            return null;
        }
    }
}
