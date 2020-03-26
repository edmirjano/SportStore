using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;
using WebUI.Models;
namespace WebUI.Controllers
{


    public class ProductController : Controller
    {
        private IProductsRepository repository;
        public int PageSize = 4;
        public ProductController(IProductsRepository productRepository) { this.repository = productRepository; }

        public ActionResult List(string Category, int Page = 1)
        {
            ProductListViewModel model = new ProductListViewModel
            {
                Products = repository.Products
                .Where(p => Category == null || p.Category == Category)
                .OrderBy(p => p.ProductID)
                .Skip((Page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    ItemsPerPage = PageSize,
                    TotalItems = Category == null ? repository.Products.Count() : repository.Products.Where(e => e.Category == Category).Count()
                },
                CurrentCategory = Category
            };
            return View(model);
        }
        public ActionResult SingleProduct(int ProductId)
        {
            var prod = repository.Products.Where(p => p.ProductID == ProductId);
            ViewBag.Prod = prod;
            return View();
        }
        public FileContentResult GetImage(int productId)
        {
            Product prod = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if (prod != null)
            {
                return File(prod.ImageData, prod.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}