﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsWithRouting.Models;
using System.Diagnostics;
using ProductsWithRouting.Services;

namespace ProductsWithRouting.Controllers
{
    public class UsersController : Controller
    {
        private List<User> myUsers;

        public UsersController(Data data)
        {
            myUsers = data.Users;
        }

        public IActionResult Index(string id)
        {
            if (id == "df2323eoT")
            {
                return View(myUsers);
            }
            else
                return RedirectToAction("UnauthorizedAction", "users", null);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult UnauthorizedAction()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AdminLogin(AdminLogin admin)
        {
            return RedirectToAction("Index", "users", new { id = admin.Login });
        }
    }
}