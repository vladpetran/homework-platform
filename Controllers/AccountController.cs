using BusinessLayer.EmailNotificationService;
using BusinessLayer.Models;
using BusinessLayer.ModelServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EducationalPortal.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        [AllowAnonymousOnly]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymousOnly]
        public ActionResult Register()
        {
            TeacherService ts = new TeacherService();
            List<TeacherViewModel> listTeacher = new List<TeacherViewModel>();
            listTeacher = ts.GetTeachers();
            ViewBag.Teachers = listTeacher;
            return PartialView("_register");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymousOnly]
        public ActionResult Register(StudentViewModel student)
        {
            if (student.Email.IsValidEmailAddress())
            {
                try
                {
                    StudentService regViewModel = new StudentService();
                    regViewModel.AddStudent(student);
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                    return View("Register", student);
                }
                return View("Register3", student);
            }
            else
            {
                TeacherService ts = new TeacherService();
                List<TeacherViewModel> listTeacher = new List<TeacherViewModel>();
                listTeacher = ts.GetTeachers();
                ViewBag.Teachers = listTeacher;
                ModelState.AddModelError("","");
                return PartialView("_register");
            }
        }

        [HttpGet]
        public ActionResult ValidationAccount(string userName)
        {
            EmailNotification notification = new EmailNotification(new EmailSender());
            StudentService instance = new StudentService();
            StudentInformationBL student = instance.GetStudentByStudentName(userName);

            string studentName = userName;
            string email = student.Email;
            int id = student.StudentID;

            notification.SendValidationEmail(email, studentName, id);
            return View();
        }
        [HttpGet]
        public ActionResult ActivateAccount(int id)
        {
            StudentService service = new StudentService();
            service.UpdateStudentByID(id);
            return View("Student");
        }
        [HttpGet]
        [AllowAnonymousOnly]
        public ActionResult Login()
        {
            return PartialView("_login");
        }
        [HttpPost]
        [AllowAnonymousOnly]
        public string Login(BusinessLayer.Models.LogInViewModel User)
        {
            if (ModelState.IsValid)
            {
                if (User.IsOK(User.UserName, User.Password))
                {
                    HttpCookie LogInCookie = new HttpCookie("UserSettings");
                    LogInCookie["UserName"] = User.UserName;
                    string role = User.SetRoleCookie(User.UserName);
                    LogInCookie["Role"] = role;
                    if (role == "Student")
                    {
                        bool confirm = User.SetConfirmCookie(User.UserName);
                        if (confirm == true)
                        {
                            LogInCookie["Confirmed"] = "Yes";
                        }
                        else
                        {
                            LogInCookie["Confirmed"] = "No";
                        }
                    }
                    int id = User.SetIDCookie(User.UserName);
                    LogInCookie["UserID"] = id.ToString();
                    Response.Cookies.Add(LogInCookie);
                    return "success";

                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                }
            }
            return "fail";
        }

        public ActionResult Logout()
        {
            if (Request.Cookies["UserSettings"] != null)
            {
                Response.Cookies["UserSettings"].Expires = DateTime.Now.AddDays(-1);
            }

            return RedirectToAction("Index", "Account");
        }

    }


}