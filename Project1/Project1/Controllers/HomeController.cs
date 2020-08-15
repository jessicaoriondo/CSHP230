using Website.Business;
using Microsoft.AspNetCore.Mvc;
using Website.Project1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Website.Project1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IClassManager classManager;

        public HomeController(IClassManager classManager)
        {
            this.classManager = classManager;
        }

        //public HomeController() 
        //{
        //    HomeController c = new HomeController(classManager);

        //}

        public ActionResult Index()
        {
            //var classes = classManager.Classes
            //                            .Select(t => new Project1.Models.ClassModel(t.Id, t.Name, t.Description, t.Price))
            //                            .ToArray();
            //var myClasses = classManager.myClasses(1).Select(t => new Project1.Models.ClassModel(t.Id, t.Name, t.Description, t.Price))
            //                            .ToArray();
            //var model = new ClassesViewModel { Classes = myClasses };
            return View();
        }

        //public ActionResult Classes(int id)
        //{
        //    var classes = classManager.myClasses()
        //    return View(model);
        //}

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}