using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project1.Business;
using Project1.Website.Models;

namespace Project1.Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly IClassManager classManager;
        private readonly IUserManager userManager;
        public HomeController(IClassManager classManager, IUserManager userManager)
        {
            this.classManager = classManager;
            this.userManager = userManager;
        }

        public ActionResult Index()
        {
            var classes = classManager.Classes
                                        .Select(t => new Project1.Website.Models.ClassModel(t.Id, t.Name, t.Description, t.Price))
                                        .ToArray();

            
            classManager.addClass(1, 2);
            classManager.addClass(2, 1);

            var myClasses = classManager.myClasses(2).Select(t => new Project1.Website.Models.ClassModel(t.Id, t.Name, t.Description, t.Price))
                                       .ToArray();
            var model = new ClassesViewModel { Classes = myClasses };
            return View(model);
        }

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpGet]
        public ActionResult register()
        {
            var newUser = new Project1.Website.Models.UserRegistrationModel();
            return View(newUser);
        }

        [HttpPost]
        public ActionResult register(UserRegistrationModel newUser)
        {
            if (userManager.exist(newUser.Email))
            {
                ViewBag.DuplicateEmailMessage = "User already exist.";
                return View();
            }
            userManager.Register(newUser.Email, newUser.Password, newUser.isAdmin);


            ViewBag.successMessage = "Registration Successful";

            return Redirect("~/Home/Login");

        }

        [HttpPost]
        public ActionResult LogIn(LoginModel loginModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.LogIn(loginModel.UserName, loginModel.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "User name and password do not match.");
                }
                else
                {
                    Session["User"] = new Project1.Website.Models.UserModel
                    {
                        Id = user.Id,
                        Name = user.Name
                    };

                    System.Web.Security.FormsAuthentication.SetAuthCookie(
                        loginModel.UserName, false);

                    return Redirect(returnUrl ?? "~/");
                }
            }

            return View(loginModel);
        }

        public ActionResult LogOff()
        {
            Session["User"] = null;
            System.Web.Security.FormsAuthentication.SignOut();

            return Redirect("~/");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your application contact page.";

            return View();
        }

        public ActionResult Classes()
        {
            var classes = classManager.Classes
                                        .Select(t => new Project1.Website.Models.ClassModel(t.Id, t.Name, t.Description, t.Price))
                                        .ToArray();
            var model = new ClassesViewModel { Classes = classes };

            ViewBag.title = "Classes";

            return View(model);
        }

        [Authorize]
        public ActionResult getMyClasses()
        {
            var user = (Project1.Website.Models.UserModel)Session["User"];
            var myClasses = classManager.myClasses(user.Id).Select(t => new Project1.Website.Models.ClassModel(t.Id, t.Name, t.Description, t.Price))
                                    .ToArray();
            var model = new ClassesViewModel { Classes = myClasses };

            ViewBag.title = "Student Classes";
            return View(model);
            
        }


        [Authorize]
        [HttpGet]
        public ActionResult Enroll()
        {
            var user = (Project1.Website.Models.UserModel)Session["User"];
            var classes = classManager.Classes
                            .Select(t => new Project1.Website.Models.ClassModel(t.Id, t.Name, t.Description, t.Price))
                            .ToList();
            var myClasses = classManager.myClasses(user.Id).Select(t => new Project1.Website.Models.ClassModel(t.Id, t.Name, t.Description, t.Price))
                                    .ToArray();

            //remove all classes that a user is already registered at using LIst
            classes.RemoveAll(x => myClasses.Any(y => y.Name == x.Name));
            var options = classes.ToArray();

            var model = new ClassRegistrationModel { myClasses = options, myClassIdToAdd = 0};
            
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Enroll(ClassRegistrationModel model)
        {
            var user = (Project1.Website.Models.UserModel)Session["User"];
            var newClassToAdd = model;


            classManager.addClass(newClassToAdd.myClassIdToAdd, user.Id);

            var classes = classManager.Classes
                           .Select(t => new Project1.Website.Models.ClassModel(t.Id, t.Name, t.Description, t.Price))
                           .ToList();
            var myClasses = classManager.myClasses(user.Id).Select(t => new Project1.Website.Models.ClassModel(t.Id, t.Name, t.Description, t.Price))
                                    .ToArray();

            //remove all classes that a user is already registered at using LIst
            classes.RemoveAll(x => myClasses.Any(y => y.Name == x.Name));
            var options = classes.ToArray();
            var NewModel = new ClassRegistrationModel { myClasses = options, myClassIdToAdd = 0 };

            ViewBag.successMessage = "Registration Successful";

            return View(NewModel);
        }

    }
}