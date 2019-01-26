using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    public class AddGrade
    {
        public int SHomeID { get; set; }
        public int HomeworkID { get; set; }
        public string StatusID { get; set; }
        public int StudentID { get; set; }
        public int Grade { get; set; }
    }
}