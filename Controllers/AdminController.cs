using BusinessLayer.Models;
using BusinessLayer.ModelServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EducationalPortal.Controllers
{
    public class AdminController : Controller
    {

        [RoleAuthorize("Administrator")]
        public ActionResult Teachers()
        {
            List<TeacherViewModel> teacherViewList = new List<TeacherViewModel>();
            TeacherService te = new TeacherService();
            teacherViewList = te.GetTeachers();
            return View(teacherViewList);
        }
        [RoleAuthorize("Administrator")]
        public ActionResult CreateTeacher()
        {
            return View();
        }
        [RoleAuthorize("Administrator")]
        [HttpPost]//by admin
        public ActionResult CreateTeacher(TeacherViewModel teacher)
        {
            TeacherService regViewModel = new TeacherService();
            try
            {
                if (ModelState.IsValid)
                {
                    regViewModel.AddTeacher(teacher);
                    return RedirectToAction("Index", "Home", teacher);
                }
            }
            catch (Exception)
            {

                return View("ErrorRegister");
            }
            return View();
        }
        [RoleAuthorize("Administrator")]
        [HttpGet]
        public ActionResult ChangeTeacher()
        {
            TeacherService ts = new TeacherService();
            List<TeacherViewModel> listTeacher = new List<TeacherViewModel>();
            listTeacher = ts.GetTeachers();
            ViewBag.Teachers = listTeacher;
            List<StudInfoViewModel> studentsList = new List<StudInfoViewModel>();
            StudentService studentService = new StudentService();
            studentsList = studentService.GetAllStudents();

            return View(studentsList);
        }
        [RoleAuthorize("Administrator")]
        [HttpPost]
        public ActionResult UpdateTeacher(int teacherID, int studentID)
        {
            StudentService stService = new StudentService();
            stService.UpdateStudentTeacher(studentID, teacherID);
            return RedirectToAction("ChangeTeacher");
        }
        [RoleAuthorize("Administrator")]
        public ActionResult GetStudent(int studentId)
        {
            TeacherService ts = new TeacherService();
            List<TeacherViewModel> listTeacher = new List<TeacherViewModel>();
            listTeacher = ts.GetTeachers();
            ViewBag.Teachers = listTeacher;
            StudInfoViewModel studentInfo = new StudInfoViewModel();
            StudentService studentService = new StudentService();
            studentInfo = studentService.GetStudByID(studentId);

            return View(studentInfo);
        }

        [RoleAuthorize("Administrator")]
        public ActionResult MyHomeworks(int id)
        {
            List<HomeworkViewModel> teacherViewList = new List<HomeworkViewModel>();
            HomeworkService te = new HomeworkService();
            teacherViewList = te.GetHomeworksByTeacherID(id);
            return View(teacherViewList);

        }
        [RoleAuthorize("Administrator")]
        public ActionResult MyStudents(int id)
        {
            List<StudInfoViewModel> studentViewList = new List<StudInfoViewModel>();
            StudentService student = new StudentService();
            studentViewList = student.GetStudentsByTeacherID(id);
            return View(studentViewList);
        }
        [HttpGet]
        [RoleAuthorize("Administrator")]
        public ActionResult ResetPass()
        {
            return View();
        }
        [HttpPost]
        [RoleAuthorize("Administrator")]
        public ActionResult ResetPass(string email)
        {

            UserViewModel User = new UserViewModel();
            bool ChangePass = User.ResetPass(email);
            ViewBag.Reset = ChangePass;
            return View("ResetPass");
        }


    }
}