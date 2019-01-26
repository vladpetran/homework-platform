using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    public class AdminViewModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Role UserRole { get; set; }
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public string HomeAdress { get; set; }
        public bool EmailConfirmation { get; set; }
    }
}