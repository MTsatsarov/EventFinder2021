namespace EventFinder2021.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public class SearchController : Controller
    {
        [HttpGet]
        public IActionResult GetEventByCityAndCategory()
        {
            return this.View();
        }
    }
}
