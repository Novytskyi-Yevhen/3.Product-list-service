using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductsWithRouting.Models;
using ProductsWithRouting.Services;

namespace ProductsWithRouting.Controllers
{
    public class ProductsController : Controller
    {
        private List<Product> myProducts;

        public ProductsController(Data data)
        {
            myProducts = data.Products;
        }
        [Route("/products/index")]
        [Route("/items/index")]
        [Route("/products")]
        [Route("/items")]
        public IActionResult Index(int filterId, string filtername)
        {
            return View(myProducts);
        }
        [Route("/products/{id?}")]
        public IActionResult View(int id)
        {
            if (id < 1 || id > myProducts.Count)
            {
                return RedirectToAction("Error");
            }
            return View(myProducts.Find(p => p.Id == id));
        }
        [HttpGet]
        [Route("/products/edit/{id?}")]
        public IActionResult Edit(int id = 1)
        {
            if (id < 1 || id > myProducts.Count)
            {
                return RedirectToAction("Error");
            }
            ViewBag.myProducts = myProducts;
            return View(myProducts.Find(p => p.Id == id));
        } 
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            ViewBag.myProducts = myProducts;
            int ind = myProducts.FindIndex(p => p.Id == product.Id);
            if (ind < 0)
            {
                return RedirectToAction("Error");
            }
            myProducts[ind] = product;
            return View(myProducts[ind]);
        }
        [HttpGet]
        [Route("/products/create")]
        [Route("/products/new")]
        public IActionResult Create()
        {
            return View(new Product() { Id = myProducts.Count + 1 });
        }
        [HttpPost]
        [Route("/products/create")]
        [Route("/products/new")]
        public IActionResult Create(Product product)
        {
            if (!myProducts.Exists(p => p.Id == product.Id))
            {
                myProducts.Add(product);
            }
            return View(myProducts.Last());
        }
        [Route("/products/delete/{id?}")]
        public IActionResult Delete(int id)
        {
            int ind = myProducts.FindIndex(p => p.Id == id);
            if (ind < 0)
            {
                return RedirectToAction("Error");
            }
            myProducts.RemoveAt(ind);
            return RedirectToAction("Index", myProducts);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
