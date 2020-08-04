using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ziggle.WebSite.Models;
using Ziggle.Business;

namespace Ziggle.WebSite.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var categories = categoryManager.Categories
                                        .Select(t => new Ziggle.WebSite.Models.CategoryModel(t.Id, t.Name))
                                        .ToArray();
            var model = new IndexModel { Categories = categories };
            return View(model);
        }

        private readonly ICategoryManager categoryManager;
        public HomeController(ICategoryManager categoryManager)
        {
            this.categoryManager = categoryManager;
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
