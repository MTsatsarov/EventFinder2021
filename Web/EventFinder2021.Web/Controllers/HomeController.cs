namespace EventFinder2021.Web.Controllers
{
    using System.Diagnostics;

    using EventFinder2021.Services.Data.EventService;
    using EventFinder2021.Web.ViewModels;
    using EventFinder2021.Web.ViewModels.EventViewModels;
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

        public IActionResult Index(int id = 1)
        {
            const int numbersPerPage = 12;
            var viewModel = new ListEventViewModel()
            {
                ItemsPerPage = numbersPerPage,
                PageNumber = id,
                Events = this.eventService.GetAllEvents(id, numbersPerPage),
                RecipeCount = this.eventService.GetCount(),
            };

            return this.View(viewModel);
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
