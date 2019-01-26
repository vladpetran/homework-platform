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
    public class HomeworkRepository
    {
        public List<Homework> GetAllHomework()
        {
            var homeworkList = new List<Homework>();

            string cs = ConfigurationManager.ConnectionStrings["TeamB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open(); //Deschidere conexiune
                SqlCommand command = new SqlCommand();
                command.CommandText = "spGetAllHomeworks";
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = connection;


                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var homework = new Homework
                        {
                            StartDate = Convert.ToDateTime(reader["StartDate"]),
                            EndDate = Convert.ToDateTime(reader["EndDate"]),

                        };
                        homeworkList.Add(homework);
                    }
                }

            }
            return homeworkList;
        }


        public List<Homework> GetAllHomeworksByTeacherID(int id)
        {
            var homeworkTeacherList = new List<Homework>();
            String cs = ConfigurationManager.ConnectionStrings["TeamB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "spGetHomeworkByID";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID", id);
                command.Connection = connection;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var homework = new Homework
                        {
                            StartDate = reader.GetDateTime(reader.GetOrdinal("StartDate")),
                            EndDate = reader.GetDateTime(reader.GetOrdinal("EndDate")),
                            Requirements = reader["Requirements"].ToString(),
                        };
                        homeworkTeacherList.Add(homework);
                    }

                }
            }
            return homeworkTeacherList;
        }


        public void AddHomework(Homework homework)
        {
            string cs = ConfigurationManager.ConnectionStrings["TeamB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open(); //Deschidere conexiune
                SqlCommand command = new SqlCommand();
                command.CommandText = "spAddHomework";
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = connection;
                command.Parameters.Add(new SqlParameter("@EndDate", homework.EndDate));
                command.Parameters.Add(new SqlParameter("@TID", homework.TeacherID));
                command.Parameters.Add(new SqlParameter("@Req", homework.Requirements));
                command.ExecuteNonQuery();
            }

        }

        public void UpdateHomework(Homework homework)
        {
            string cs = ConfigurationManager.ConnectionStrings["TeamB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open(); //Deschidere conexiune
                SqlCommand command = new SqlCommand();
                command.CommandText = "spUpdateHomework";
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = connection;
                command.Parameters.Add(new SqlParameter("@Start", SqlDbType.DateTime) { Value = homework.StartDate });
                command.Parameters.Add(new SqlParameter("@End", SqlDbType.DateTime) { Value = homework.EndDate });
                command.Parameters.Add(new SqlParameter("@pReq", SqlDbType.NVarChar) { Value = homework.Requirements });
                command.ExecuteNonQuery();
            }

        }


        public void DeleteHomework(int id)
        {
            string cs = ConfigurationManager.ConnectionStrings["TeamB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open(); //Deschidere conexiune
                SqlCommand command = new SqlCommand();
                command.CommandText = "spDeleteHomework";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@pID", id);
                if (command.ExecuteNonQuery() > 0)
                    Console.WriteLine("Succeessfully deleted");

                else
                    Console.WriteLine("Row not deleted");
                connection.Close();
            }
        }
        //get a homework by an ID
        public Homework GetHomeworkByHomeworkID(int id)
        {
            string cs = ConfigurationManager.ConnectionStrings["TeamB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "spGetHomeworkByHomeworkID";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID", id);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var homeworkId = new Homework
                        {
                            StartDate = reader.GetDateTime(reader.GetOrdinal("StartDate")),
                            EndDate = reader.GetDateTime(reader.GetOrdinal("EndDate")),
                            Requirements = reader["Requirements"].ToString(),

                        };
                        if (homeworkId != null)
                            return homeworkId;
                    }
                }

            }
            return null;
        }


    }
}

