using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLayer.Repository;
using BusinessLayer.Models;
using DataAccessLayer.Entities;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.ModelServices
{
    public class TeacherService
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string RoleID { get; set; }
        [Display(Name = "First Name")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Home Address")]
        public string HomeAddress { get; set; }
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public void AddTeacher(TeacherViewModel teacher)
        {
            TeacherRepository repo = new TeacherRepository();
            Teacher newTeacher = new Teacher();

            newTeacher.UserName = teacher.UserName;
            newTeacher.Password = teacher.Password;
            newTeacher.Email = teacher.Email;
            newTeacher.PhoneNumber = teacher.PhoneNumber;
            newTeacher.FirstName = teacher.FirstName;
            newTeacher.LastName = teacher.LastName;
            newTeacher.HomeAddress = teacher.HomeAddress;
            newTeacher.RoleID = "Teacher";
            repo.AddTeacher(newTeacher);
        }


        public void UpdateTeacher(TeacherViewModel teacher)
        {
            TeacherRepository repo = new TeacherRepository();
            Teacher newTeacher = new Teacher()
            {
                UserName = teacher.UserName,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Email = teacher.Email,
                PhoneNumber = teacher.PhoneNumber,
                HomeAddress = teacher.HomeAddress,
                TeacherID = teacher.TeacherID
            };
            repo.UpdateTeacher(newTeacher);

        }
        public List<TeacherViewModel> GetTeachers()
        {
            TeacherRepository repo = new TeacherRepository();
            List<Teacher> teacherList = repo.GetAllTeachers();
            List<TeacherViewModel> newList = new List<TeacherViewModel>();
            foreach (var item in teacherList)
            {
                TeacherViewModel newTeacher = new TeacherViewModel()
                {
                    UserName = item.UserName,
                    Email = item.Email,
                    TeacherID = item.TeacherID,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    HomeAddress = item.HomeAddress,
                    PhoneNumber = item.PhoneNumber,

                };

                newList.Add(newTeacher);
            }
            return newList;
        }



        public TeacherViewModel GetTeacherByTeacherName(string teacherName)
        {
            TeacherRepository repository = new TeacherRepository();
            Teacher newTeacher = repository.GetTeacherByTeacherName(teacherName);

            TeacherViewModel teacher = new TeacherViewModel()
            {
                TeacherID = newTeacher.TeacherID,
                Email = newTeacher.Email,
                UserName = teacherName,
                FirstName = newTeacher.FirstName,
                LastName = newTeacher.LastName,
                HomeAddress = newTeacher.HomeAddress,
                PhoneNumber = newTeacher.PhoneNumber
            };

            return teacher;
        }
    }






}
