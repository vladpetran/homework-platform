using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Entities
{
    public class Student 
    {
        public int StudentID { get; set; }        
        public int TeacherID { get; set; }
        [Required(ErrorMessage="Camp obligatoriu")]
        [StringLength(5,ErrorMessage="Numele de utilizator trebuie sa aiba minim 5 caractere")]
        public string UserName { get; set; }
        public string Password { get; set; }
        [Required(ErrorMessage="Introduceti o adresa de email")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
        public string RoleID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HomeAddress { get; set; }
        public string PhoneNumber { get; set; }
        public bool EmailConfirmation { get; set; }

    }
}