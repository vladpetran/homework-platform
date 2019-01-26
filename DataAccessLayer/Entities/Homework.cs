using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Entities
{
    public class Homework
    {
        public int HomeworkID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TeacherID { get; set; }
        public string Requirements { get; set; }

        public Homework(DateTime StartDate, DateTime EndDate, int TeacherID, string Requirements)
        {
            this.EndDate = EndDate;
            this.StartDate = StartDate;
            this.Requirements = Requirements;
            this.TeacherID = TeacherID;
        }
        public Homework()
        {

        }
    }

}