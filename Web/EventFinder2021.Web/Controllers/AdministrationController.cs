﻿namespace EventFinder2021.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class AdministrationController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
