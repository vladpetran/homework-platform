using BusinessLayer.Models;
using BusinessLayer.RegisterStudentService;
using DataAccessLayer.Entities;
using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusinessLayer.RegisterUserService
{
    public class UserServices
    {
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Role UserRole { get; set; }
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public string HomeAdress { get; set; }
        [DisplayName("Numele Profesorului")]
        public int TeacherID { get; set; }

        public UserServices(string userName, string password, string email, string phoneNumber, string userRole, string firstName, string lastName, string homeAdress, int teacherID)
        {
            this.UserName = userName;
            this.Password = password;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.UserRole = UserRole;
            this.Firstname = firstName;
            this.LastName = lastName;
            this.HomeAdress = homeAdress;
            this.TeacherID = teacherID;
        }
        public UserServices()
        {
        }
        public void AddUsers(UserServices item)
        {
            StudentRepository repo = new StudentRepository();
            var reg = new Register();
            Student newStudent = new Student();
            newStudent.UserName = item.UserName;
            newStudent.Password = item.Password;
            newStudent.Email = item.Email;
            newStudent.PhoneNumber = item.PhoneNumber;
            newStudent.EmailConfirmation = false;
            newStudent.FirstName = item.Firstname;
            newStudent.LastName = item.LastName;
            newStudent.HomeAddress = item.HomeAdress;
            newStudent.TeacherID = item.TeacherID;
            repo.AddStudent(newStudent);
            //  reg.Add(newStudent);
        }
    }
}
