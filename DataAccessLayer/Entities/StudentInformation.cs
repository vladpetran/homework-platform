using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Entities
{
    public class StudentInformationDAL
    {
        public int StudentID { get; set; }
        public string Email { get; set; }
        public string StudentName { get; set; }
    }
}