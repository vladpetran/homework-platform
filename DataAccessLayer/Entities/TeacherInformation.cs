using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Entities
{
    public class TeacherInformationDAL
    {
        public int TeacherID { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}