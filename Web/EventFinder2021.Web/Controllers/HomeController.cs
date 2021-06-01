namespace EventFinder2021.Web.Controllers
{
    using System.Diagnostics;

    using EventFinder2021.Services.Data.EventService;
    using EventFinder2021.Web.ViewModels;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly IEventService eventService;

        public HomeController(IWebHostEnvironment hostEnvironment, IEventService eventService)
        {
            this.hostEnvironment = hostEnvironment;
            this.eventService = eventService;
        }

        public IActionResult Index()
        {
            var path = this.hostEnvironment.WebRootPath;
            var models = this.eventService.GetAllEvents(path);
            return this.View(models);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
