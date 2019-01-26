using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    public class StudInfoViewModel
    {
        public int StudentID { get; set; }
        [Display(Name="First Name")]
        public string FirstName { get; set; }
        [Display(Name="Last Name")]
        public string LastName { get; set; }
        public string Email { get; set; }
        [Display(Name="Phone No.")]
        public string PhoneNumber { get; set; }
        public int TeacherID { get; set; }
        [Display(Name="Grade Average")]
        public double GradeAverage { get; set; }
        public string StatusId { get; set; }

        public StudInfoViewModel()
        {

        }
        public StudInfoViewModel(int StudentID, string FirstName, string LastName, string Email, string PhoneNumber)
        {
            this.StudentID = StudentID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.PhoneNumber = PhoneNumber;
        }
        public StudInfoViewModel(int StudentID, string FirstName, string LastName, string Email, string PhoneNumber, int TeacherID)
        {
            this.StudentID = StudentID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.PhoneNumber = PhoneNumber;
            this.TeacherID = TeacherID;
        }
    }
}