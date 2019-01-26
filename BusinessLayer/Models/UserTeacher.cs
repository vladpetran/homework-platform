using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    public class UserTeacher
    {
        public int TeacherID { get; set; }
        public string UserName { get; set; }
        public string RoleID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HomeAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
    }
}