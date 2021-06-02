namespace EventFinder2021.Web.Controllers
{
    using System.Diagnostics;

    using EventFinder2021.Common;
    using EventFinder2021.Services.Data.EventService;
    using EventFinder2021.Web.ViewModels;
    using EventFinder2021.Web.ViewModels.EventViewModels;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IEventService eventService;

        public HomeController(IEventService eventService)
        {
            this.eventService = eventService;
        }

        public IActionResult Index(int id = 1)
        {
            var viewModel = new ListEventViewModel()
            {
                ItemsPerPage = GlobalConstants.ItemsPerPage,
                PageNumber = id,
                Events = this.eventService.GetAllEvents(id, GlobalConstants.ItemsPerPage),
                EventsCount = this.eventService.GetCount(),
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
