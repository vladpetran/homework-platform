using BusinessLayer.Models;
using BusinessLayer.ModelServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EducationalPortal.Controllers
{
    public class StudentHomeworksController : Controller
    {
        [HttpPost]
        [RoleAuthorize("Student")]
        public ActionResult UploadHomeworkFiles(StudentHomeworkViewModel sh)
        {
            if (sh.files.ElementAt(0) == null)
            {
                return RedirectToAction("Homeworks", "Student");
            }
            string username = Request.Cookies["UserSettings"].Values["UserName"];

            string dir = "~/Homeworks/" + username + "/" + sh.SHomeID;

            bool exists = System.IO.Directory.Exists(Server.MapPath(dir.ToString()));
            string myPath = sh.Path;

            foreach (var file in sh.files)
            {
                if (file != null)
                {
                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(dir.ToString()));

                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath(dir.ToString()), fileName);
                    file.SaveAs(path);
                    myPath = fileName + " " + myPath;
                }
            }
            StudentHomeworkServices HService = new StudentHomeworkServices();
            HService.UpdateStudentHomework(sh.SHomeID, 0, "Uploaded", myPath);

            return RedirectToAction("Homeworks", "Student");
        }
        [RoleAuthorize("Teacher")]
        public ActionResult ViewDetails(int studentId, int sHId)
        {
            StudentHomeworkServices service = new StudentHomeworkServices();
            StudentHomeworkViewModel homework = service.GetHomeworkDetails(studentId, sHId);

            return View(homework);
        }
        [RoleAuthorize("Teacher")]
        public ActionResult GetFiles(int studentId, int sHId, string studentName)
        {

            string path = "~/Homeworks/" + studentName + "/" + sHId + "/";
            var dir = new System.IO.DirectoryInfo(Server.MapPath(path));
            System.IO.FileInfo[] fileNames = dir.GetFiles("*.*");
            List<string> items = new List<string>();

            foreach (var file in fileNames)
            {
                items.Add(file.Name);
            }
            ViewBag.HomeID = sHId;
            ViewBag.StudentName = studentName;
            return View(items);
        }
        [RoleAuthorize("Teacher")]
        public ActionResult OpenFile(int homeID, string FileName, string StudentName)
        {

            string content = string.Empty;
            string path = "~/Homeworks/" + StudentName + "/" + homeID + "/" + FileName;
            using (StreamReader reader = new StreamReader(Server.MapPath(path)))
            {
                content = reader.ReadToEnd();
            }

            ViewBag.Content = content;

            var text = Request.Form["TextAreaName"];



            return View("GetFiles2");
        }

        [RoleAuthorize("Teacher")]
        public ActionResult GetPdf(int homeID, string fileName, string StudentName)
        {
            string path = "~/Homeworks/" + StudentName + "/" + homeID + "/" + fileName;
            var fileStream = new FileStream(Server.MapPath(path),
                                             FileMode.Open,
                                             FileAccess.ReadWrite
                                           );
            var fsResult = new FileStreamResult(fileStream, "text/plain");

            return fsResult;
        }
        [RoleAuthorize("Teacher")]
        public FileResult Download(int homeID, string FileName, string StudentName)
        {
            string path = "~/Homeworks/" + StudentName + "/" + homeID + "/";
            return File(path + FileName, System.Net.Mime.MediaTypeNames.Application.Octet);
        }

    }
}