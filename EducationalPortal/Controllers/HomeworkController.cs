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
    public class HomeworkController : Controller
    {
        [RoleAuthorize("Teacher")]
        public ActionResult Create()
        {
            return View();
        }



        [HttpPost]
        [RoleAuthorize("Teacher")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HomeworkViewModel homework)
        {
            if (homework.endDate <= DateTime.Now)
            {
                ModelState.AddModelError("", "Please choose another end date!");
                return View();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    HomeworkService service = new HomeworkService();
                    int id = Int32.Parse(Request.Cookies["UserSettings"].Values["UserID"]);
                    homework.TeacherID = id;
                    service.AddHomework(homework);
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                    return View();
                }
                try
                {
                    StudentService studentService = new StudentService();
                    int id = Int32.Parse(Request.Cookies["UserSettings"].Values["UserID"]);
                    List<StudInfoViewModel> studentsList = studentService.GetStudentsByTeacherID(id);//all students

                    EmailNotification notification = new EmailNotification(new EmailSender());

                    foreach (var student in studentsList)
                    {
                        notification.NotifystudentWhenHomeworkIsCreated(student.Email, student.FirstName, homework.endDate);

                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Unable to send email to all students. Try again, and if the problem persists, see your system administrator.");
                    return RedirectToAction("MyHomeworks", "Teacher");
                }


            }


            return RedirectToAction("MyHomeworks", "Teacher");

        }


    }
}