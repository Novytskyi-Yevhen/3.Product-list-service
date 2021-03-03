using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProductsWithRouting.Filters;
using ProductsWithRouting.Models;
using ProductsWithRouting.Services;
using ProductsWithRouting.Services.Interfaces;

namespace ProductsWithRouting.Controllers
{
    public class ProductsController : Controller
    {
        private List<Product> myProducts;
        private IProductFilter productFilter;

        public ProductsController(Data data, IProductFilter filter)
        {
            myProducts = data.Products;
            productFilter = filter;
        }
        [Route("/products/index")]
        [Route("/items/index")]
        [Route("/products")]
        [Route("/items")]
        public IActionResult Index(Product product)
        {
            return View(productFilter.FilterBy(product));
        }
        [Route("/products/{id}")]
        [ValidateProductExists]
        public IActionResult View(int id)
        {
            return View(myProducts.Find(p => p.Id == id));
        }
        [HttpGet]
        [Route("/products/edit/{id}")]
        [ValidateProductExists]
        public IActionResult Edit(int id)
        {
            return View(myProducts.Find(p => p.Id == id));
        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            int ind = myProducts.FindIndex(p => p.Id == product.Id);
            myProducts[ind] = product;
            return View();
        }
        [HttpGet]
        [Route("/products/create")]
        [Route("/products/new")]
        public IActionResult Create()
        {
            return View(new Product());
        }
        [HttpPost]
        [Route("/products/create")]
        [Route("/products/new")]
        public IActionResult Create(Product product)
        {
            if (myProducts.Any())
            {
                product.Id = myProducts.Max(p => p.Id) + 1;
            }
            else
            {
                product.Id = 1;
            }
            myProducts.Add(product);
            return View();
        }
        [Route("/products/delete/{id}")]
        [ValidateProductExists]
        public IActionResult Delete(int id)
        {
            int ind = myProducts.FindIndex(p => p.Id == id);
            myProducts.RemoveAt(ind);
            return RedirectToAction("Index", myProducts);
        }

        public IActionResult Error404()
        {
            return View();
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
