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

        private readonly ICategoryManager categoryManager;
        private readonly IProductManager productManager;

        public IActionResult Index()
        {
            var categories = categoryManager.Categories
                                        .Select(t => new Ziggle.WebSite.Models.CategoryModel(t.Id, t.Name))
                                        .ToArray();
            var model = new IndexModel { Categories = categories };
            return View(model);
        }


        public HomeController(ICategoryManager categoryManager, IProductManager productManager)
        {
            this.categoryManager = categoryManager;
            this.productManager = productManager;
        }

        public ActionResult Category(int id)
        {
            var category = categoryManager.Category(id);
            var products = productManager
                                .ForCategory(id)
                                .Select(t =>
                                    new Ziggle.WebSite.Models.ProductModel
                                    {
                                        Id = t.Id,
                                        Name = t.Name,
                                        Price = t.Price,
                                        Quantity = t.Quantity
                                    }).ToArray();

            var model = new CategoryViewModel
            {
                Category = new Ziggle.WebSite.Models.CategoryModel(category.Id, category.Name),
                Products = products
            };

            return View(model);
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
