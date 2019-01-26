using BusinessLayer.EmailNotificationService;
using BusinessLayer.Models;
using BusinessLayer.ModelServices;
using BusinessLayer.RegisterUserService;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
/**/
namespace EducationalPortal.Controllers
{
    public class RegisterController : Controller
    {
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
	}
}