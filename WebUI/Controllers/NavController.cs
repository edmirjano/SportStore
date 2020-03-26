using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class NavController : Controller
    {
        // GET: Nav
        private IProductsRepository repository;
        public NavController(IProductsRepository repo) { repository = repo; }
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult CatMenu(string category = null)
        {
            ViewBag.SelectedCategory = category;

            IEnumerable<string> categories = repository.Products
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x);

            return PartialView(categories);

        }
    }
}