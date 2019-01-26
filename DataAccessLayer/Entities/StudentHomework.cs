using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Entities
{
    public class StudentHomework
    {
        public int SHomeID { get; set; }
        public int HomeworkID { get; set; }
        public string StatusID { get; set; }
        public int StudentID { get; set; }
        public string UploadedFiles { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Requirements { get; set; }
        public int Grade { get; set; }
        public String Comments { get; set; }
    
    }
}