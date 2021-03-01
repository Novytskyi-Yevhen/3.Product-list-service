﻿using System;
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

        public IActionResult Index(int filterId, string filtername)
        {
            return View(myProducts);
        }

        public IActionResult View(int id)
        {
            return View(myProducts.Find(p => p.Id == id));
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(myProducts.Find(p => p.Id == id));
        } 
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            int ind = myProducts.FindIndex(p => p.Id == product.Id);
            myProducts[ind] = product;
            return View(myProducts[ind]);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new Product() { Id = myProducts.Count + 1 });
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (!myProducts.Exists(p => p.Id == product.Id))
            {
                myProducts.Add(product);
            }
            return View(myProducts.Last());
        }

        public IActionResult Delete(int id)
        {
            //Please, add your implementation of the method
            return View("Index"/*TODO: pass corresponding product here*/);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
