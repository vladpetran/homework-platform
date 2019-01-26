using DataAccessLayer.Entities;
using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    public class StudentViewModel
    {
        public int StudentID { get; set; }
        public int TeacherID { get; set; }
        [Required]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^[0-9]*$",ErrorMessage="Phone number is not valid")]
        public string PhoneNumber { get; set; }
        public Role UserRole { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Firstname { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string LastName { get; set; }
        [Required]
        public string HomeAdress { get; set; }
        public bool EmailConfirmation { get; set; }
        public IEnumerable<HttpPostedFileBase> files { get; set; }
    }
}