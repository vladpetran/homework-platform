using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    public class AddComments
    {
        public int SHomeID { get; set; }
        public int HomeworkID { get; set; }
        public string StatusID { get; set; }
        public int StudentID { get; set; }
        public string Comments { get; set; }
    }
}