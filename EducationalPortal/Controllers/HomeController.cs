using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EducationalPortal;
using System.Data.Mapping;
using System.Web.Mvc;
using BusinessLayer.Models;
using System.Web.Security;

namespace EducationalPortal.Controllers
{
    public class HomeController : Controller
    {
        [RoleAuthorize("Administrator", "Student", "Teacher")]
        public ActionResult Index()
        {
            return View();
        }
      
    }
}