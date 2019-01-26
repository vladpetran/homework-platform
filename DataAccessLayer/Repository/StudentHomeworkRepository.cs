using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Repository
{
    public class StudentHomeworkRepository
    {
        //get homeworks by student id
        public static List<StudentHomework> GetStudentHomeWorks(int id)
        {
            List<StudentHomework> homeworks = new List<StudentHomework>();
            string cs = ConfigurationManager.ConnectionStrings["TeamB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "spGetStudentHomeworks";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID", id);
                command.Connection = connection;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var studenthomework = new StudentHomework
                        {
                            SHomeID = Convert.ToInt32(reader["SHomeID"]),
                            HomeworkID = Convert.ToInt32(reader["HomeworkID"]),
                            StatusID = reader["StatusID"].ToString(),
                            UploadedFiles = reader["UploadedFiles"].ToString(),
                            Grade = Convert.ToInt32(reader["Grade"]),
                            StartDate = Convert.ToDateTime(reader["StartDate"]),
                            EndDate = Convert.ToDateTime(reader["EndDate"]),
                            Requirements = reader["Requirements"].ToString(),
                            Comments = reader["Comments"].ToString()
                        };

                        if (studenthomework != null)
                            homeworks.Add(studenthomework);
                    }
                    // return homeworks;
                }
            }
            return homeworks;
        }
        public static StudentHomework UpdateStudentHomeworkBySHomeworkID(int sHomeworkId, int grade, string status, string path)
        {
            string cs = ConfigurationManager.ConnectionStrings["TeamB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "spUpdateStudentHomework";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@SHID", sHomeworkId);
                command.Parameters.AddWithValue("@UpLoc", path);
                command.Parameters.AddWithValue("@Grade", grade);
                command.Parameters.AddWithValue("@StatID", status);


                command.Connection = connection;
                command.ExecuteNonQuery();

            }
            return null;
        }

        public void AddGrade(int sHomeworkId, int grade)
        {
            string cs = ConfigurationManager.ConnectionStrings["TeamB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open(); //Deschidere conexiune
                SqlCommand command = new SqlCommand();
                command.CommandText = "spAddGrade";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", sHomeworkId);
                command.Parameters.AddWithValue("@grade", grade);

                command.Connection = connection;
                command.ExecuteNonQuery();
            }

        }

        //add a comment to a homework
        public void AddComments(int sHomeworkId, string comments)
        {
            string cs = ConfigurationManager.ConnectionStrings["TeamB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open(); //Deschidere conexiune
                SqlCommand command = new SqlCommand();
                command.CommandText = "spAddComments";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@SID", sHomeworkId);
                command.Parameters.AddWithValue("@Comm", comments);

                command.Connection = connection;
                command.ExecuteNonQuery();
            }

        }
        public void Rejected(int sHomeworkId, string comments)
        {
            string cs = ConfigurationManager.ConnectionStrings["TeamB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open(); //Deschidere conexiune
                SqlCommand command = new SqlCommand();
                command.CommandText = "spReject";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@SID", sHomeworkId);
                command.Parameters.AddWithValue("@Comm", comments);
                command.Connection = connection;
                command.ExecuteNonQuery();
            }

        }
    }
}