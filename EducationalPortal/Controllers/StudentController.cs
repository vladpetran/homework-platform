using BusinessLayer.Models;
using BusinessLayer.ModelServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EducationalPortal.Controllers
{
    public class StudentController : Controller
    {


        [RoleAuthorize("Student")]
        [ConfirmAuthorize]
        public ActionResult Homeworks(int? reportID)
        {
            StudentHomeworkServices hServicies = new StudentHomeworkServices();
            int id = Int32.Parse(Request.Cookies["UserSettings"].Values["UserId"]);

            List<StudentHomeworkViewModel> List = hServicies.GetStudentHomeWorks(id);

            switch (reportID)
            {
                case 1:
                    //  information = "best";
                    List<StudentHomeworkViewModel> bestHM = List.Where(a => a.StatusID == "Accepted").OrderByDescending(a => a.Grade).Take(10).ToList();
                    return View("Homeworks2", bestHM);
                case 2:
                    //  information = "Uploaded";
                    return View("Homeworks2", List.Where(a => a.StatusID == "Uploaded"));
                case 3:
                    //   information = "Pending";
                    return View("Homeworks2", List.Where(a => a.StatusID == "Pending"));
                case 4:
                    //    information = "Accepted";
                    return View("Homeworks2", List.Where(a => a.StatusID == "Accepted"));
                case 5:
                    //  information = "Rejected";
                    return View("Homeworks2", List.Where(a => a.StatusID == "Rejected"));
                default:
                    /// information = "all";
                    return View("Homeworks", List);
            }

        }

        //
        // GET: /Student/Reports
        [ConfirmAuthorize]
        [RoleAuthorize("Student")]
        public ActionResult MyReports()
        {
            return View("MyReports");
        }

        [RoleAuthorize("Student")]
        [ConfirmAuthorize]
        public ActionResult Reports(int reportID)
        {
            StudentHomeworkServices hServicies = new StudentHomeworkServices();
            int id = Int32.Parse(Request.Cookies["UserSettings"].Values["UserId"]);

            List<StudentHomeworkViewModel> List = hServicies.GetStudentHomeWorks(id);
            switch (reportID)
            {
                case 1:
                    //  information = "best";
                    List<StudentHomeworkViewModel> bestHM = List.Where(a => a.StatusID == "Accepted").OrderByDescending(a => a.Grade).Take(10).ToList();
                    return View("_Homeworks", bestHM);
                case 2:
                    //  information = "Uploaded";
                    return View("_Homeworks", List.Where(a => a.StatusID == "Uploaded"));
                case 3:
                    //   information = "Pending";
                    return View("_Homeworks", List.Where(a => a.StatusID == "Pending"));
                case 4:
                    //    information = "Accepted";
                    return View("_Homeworks", List.Where(a => a.StatusID == "Accepted"));
                case 5:
                    //  information = "Rejected";
                    return View("_Homeworks", List.Where(a => a.StatusID == "Rejected"));
                default:
                    /// information = "all";
                    return View("_Homeworks", List);
            }
        }

        [RoleAuthorize("Student")]
        public ActionResult GeneratePDF(int reportID)
        {
            StudentHomeworkServices hServicies = new StudentHomeworkServices();
            int id = Int32.Parse(Request.Cookies["UserSettings"].Values["UserId"]);

            List<StudentHomeworkViewModel> List = hServicies.GetStudentHomeWorks(id);
            switch (reportID)
            {
                case 1:
                    //  information = "best";
                    List<StudentHomeworkViewModel> bestHM = List.Where(a => a.StatusID == "Accepted").OrderByDescending(a => a.Grade).Take(10).ToList();
                    return new Rotativa.ViewAsPdf("_Homeworks", bestHM);
                case 2:
                    //  information = "Uploaded";
                    return new Rotativa.ViewAsPdf("_Homeworks", List.Where(a => a.StatusID == "Uploaded"));
                case 3:
                    //   information = "Pending";
                    return new Rotativa.ViewAsPdf("_Homeworks", List.Where(a => a.StatusID == "Pending"));
                case 4:
                    //    information = "Accepted";
                    return new Rotativa.ViewAsPdf("_Homeworks", List.Where(a => a.StatusID == "Accepted"));
                case 5:
                    //  information = "Rejected";
                    return new Rotativa.ViewAsPdf("_Homeworks", List.Where(a => a.StatusID == "Rejected"));
                default:
                    /// information = "all";
                    return new Rotativa.ViewAsPdf("_Homeworks", List);
            }

        }

    }
}
