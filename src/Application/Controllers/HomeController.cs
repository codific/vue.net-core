﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("/error")]
        public IActionResult Error()
        {
            throw new System.Exception("big mistake");
            return View();
        }

    }
}
