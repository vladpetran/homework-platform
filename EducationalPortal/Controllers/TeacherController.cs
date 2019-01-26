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
    public class TeacherController : Controller
    {
        [RoleAuthorize("Teacher")]
        public ActionResult MyProfile()
        {
            TeacherService service = new TeacherService();
            string username = Request.Cookies["UserSettings"].Values["UserName"];
            TeacherViewModel teacherInfo = service.GetTeacherByTeacherName(username);
            teacherInfo.UserName = username;
            return View(teacherInfo);

        }
        [RoleAuthorize("Teacher")]
        public ActionResult Edit()
        {


            return View();
        }

        [HttpPost]
        [RoleAuthorize("Teacher")]
        public ActionResult Edit(TeacherViewModel teacher)
        {
            string username = Request.Cookies["UserSettings"].Values["UserName"];
            TeacherService service = new TeacherService();
            TeacherViewModel teacher2 = service.GetTeacherByTeacherName(username);

            if (teacher.UserName == "" | teacher.UserName == null)
                teacher.UserName = teacher2.UserName;
            if (teacher.FirstName == "" | teacher.FirstName == null)
                teacher.FirstName = teacher2.FirstName;
            if (teacher.LastName == "" | teacher.LastName == null)
                teacher.LastName = teacher2.LastName;
            if (teacher.Email == "" | teacher.Email == null)
                teacher.Email = teacher2.Email;
            if (teacher.PhoneNumber == "" | teacher.PhoneNumber == null)
                teacher.PhoneNumber = teacher2.PhoneNumber;
            if (teacher.HomeAddress == "" | teacher.HomeAddress == null)
                teacher.HomeAddress = teacher2.HomeAddress;
            teacher.TeacherID = teacher2.TeacherID;
            service.UpdateTeacher(teacher);
            return RedirectToAction("MyProfile");
        }

        //get all students where teacherId==loggedTeacher.ID
        [RoleAuthorize("Teacher")]
        public ActionResult MyStudents()
        {
            int id = Int32.Parse(Request.Cookies["UserSettings"].Values["UserId"]);
            List<StudInfoViewModel> studentsList = new List<StudInfoViewModel>();
            StudentService studentService = new StudentService();
            studentsList = studentService.GetStudentsByTeacherID(id);//all students
            return View(studentsList);

        }
        [RoleAuthorize("Teacher")]
        public ActionResult Reports()
        {
            return View("Reports");
        }
        [RoleAuthorize("Teacher")]
        public ActionResult GetReports(int reportId)
        {
            int id = Int32.Parse(Request.Cookies["UserSettings"].Values["UserId"]);
            List<StudInfoViewModel> studentsList = new List<StudInfoViewModel>();
            StudentService studentService = new StudentService();
            studentsList = studentService.GetStudentsByTeacherID(id);//all students

            List<HomeworkViewModel> homeworksList = new List<HomeworkViewModel>();
            HomeworkService services = new HomeworkService();
            homeworksList = services.GetHomeworksByTeacherID(id);

            switch (reportId)
            {

                case 1://best 10 students
                    List<StudInfoViewModel> bestStudents = studentsList.Where(a => a.GradeAverage > 1).OrderByDescending(a => a.GradeAverage).Take(10).ToList();
                    return View("StudentReports", bestStudents);
                case 2://Get All Students with grade smaller than 5
                    List<StudInfoViewModel> badStudents = studentsList.Where(a => a.GradeAverage < 5).ToList();
                    return View("StudentReports", badStudents);
                case 3://Get All Valable Homeworks
                    List<HomeworkViewModel> homeworks = homeworksList.Where(a => a.endDate > DateTime.Now).ToList();
                    return View("HomeworkReports", homeworks);//alt view

                default:
                    //all student reportId==null
                    return View(studentsList);
            }
        }

        //install Rotativa din Nuget
        [RoleAuthorize("Teacher")]
        public ActionResult GeneratePDF(int reportID)
        {
            int id = Int32.Parse(Request.Cookies["UserSettings"].Values["UserId"]);
            List<StudInfoViewModel> studentsList = new List<StudInfoViewModel>();
            StudentService studentService = new StudentService();
            studentsList = studentService.GetStudentsByTeacherID(id);//all students
            List<HomeworkViewModel> homeworksList = new List<HomeworkViewModel>();
            HomeworkService services = new HomeworkService();
            homeworksList = services.GetHomeworksByTeacherID(id);

            switch (reportID)
            {
                case 1:
                    //best students
                    List<StudInfoViewModel> bestStudents = studentsList.Where(a => a.GradeAverage > 1).OrderByDescending(a => a.GradeAverage).Take(10).ToList();
                    return new Rotativa.ViewAsPdf("_StudentReports", bestStudents);
                case 2:
                    //Get All Students with grade smaller than 5
                    List<StudInfoViewModel> badStudents = studentsList.Where(a => a.GradeAverage < 5).ToList();
                    return new Rotativa.ViewAsPdf("_StudentReports", badStudents);
                case 3:
                    //Get All Valable Homeworks
                    List<HomeworkViewModel> homeworks = homeworksList.Where(a => a.endDate > DateTime.Now).ToList();
                    return new Rotativa.ViewAsPdf("_HomeworkReports", homeworks);

                default:
                    /// information = "all";
                    return new Rotativa.ViewAsPdf("_StudentReports", studentsList);
            }

        }
        [RoleAuthorize("Teacher")]
        public ActionResult MyHomeworks()
        {

            int id = Int32.Parse(Request.Cookies["UserSettings"].Values["UserId"]);
            List<HomeworkViewModel> homeworksList = new List<HomeworkViewModel>();
            HomeworkService services = new HomeworkService();
            homeworksList = services.GetHomeworksByTeacherID(id);
            return View(homeworksList);
        }
        [RoleAuthorize("Teacher")]
        public ActionResult StudentHomeworks(int studentId)
        {
            StudentHomeworkServices service = new StudentHomeworkServices();
            List<StudentHomeworkViewModel> list = service.GetStudentHomeWorks(studentId);

            return View(list);
        }



        //GET
        [RoleAuthorize("Teacher")]
        public ActionResult AddGrade(int SHId, int studentId)
        {
            AddGrade item = new AddGrade();
            item.StudentID = studentId;
            item.SHomeID = SHId;
            return View(item);
        }

        //POST ACTION TO ADD A GRADE
        [HttpPost]
        [RoleAuthorize("Teacher")]
        public ActionResult AddGrade(AddGrade item)
        {
            int grade = item.Grade;
            int sHid = item.SHomeID;
            int studentId = item.StudentID;
            if ((grade >= 1) && (grade <= 10))
            {
                StudentHomeworkServices service = new StudentHomeworkServices();
                service.AddGrade(sHid, grade, studentId);
                return RedirectToAction("MyStudents", "Teacher");
            }
            else
            {
                ModelState.AddModelError("", "Unable to add a grade.The grade must bee between 1 and 10");
            }
            return View();
        }

        //GET
        [HttpGet]
        [RoleAuthorize("Teacher")]
        public ActionResult AddComments(int SHId, int studentId)
        {
            AddComments item = new AddComments();
            item.StudentID = studentId;
            item.SHomeID = SHId;
            return View(item);
        }

        //POST ACTION FOR ADDING A COMMENT
        [HttpPost]
        [RoleAuthorize("Teacher")]
        public ActionResult AddComments(AddComments item)
        {
            string comments = item.Comments;
            int sHid = item.SHomeID;
            int studentId = item.StudentID;
            if (comments != "")
            {
                StudentHomeworkServices service = new StudentHomeworkServices();
                service.AddComment(sHid, comments, studentId);
                return RedirectToAction("MyStudents", "Teacher");
            }
            else
            {
                ModelState.AddModelError("", "Please add a comment!");
            }
            return View();
        }

        [HttpGet]
        [RoleAuthorize("Teacher")]
        public ActionResult Rejected(int SHId, int studentId)
        {
            Rejected item = new Rejected();
            item.StudentID = studentId;
            item.SHomeID = SHId;
            return View(item);
        }

        [HttpPost]
        [RoleAuthorize("Teacher")]
        public ActionResult Rejected(Rejected item)
        {
            string comments = item.Comments;
            int sHid = item.SHomeID;
            int studentId = item.StudentID;
            if (comments != "")
            {
                StudentHomeworkServices service = new StudentHomeworkServices();
                service.Rejected(sHid, comments, studentId);
                return RedirectToAction("MyStudents", "Teacher");
            }
            else
            {
                ModelState.AddModelError("", "Please add a comment to the reject homework!");
            }
            return View();
        }


    }
}
