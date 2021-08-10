namespace EventFinder2021.Web.Controllers
{
    using EventFinder2021.Common;
    using EventFinder2021.Services.Data.EventService;
    using EventFinder2021.Web.ViewModels.EventViewModels;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    public class SearchController : Controller
    {
        private readonly IEventService eventService;

        public SearchController(IEventService eventService)
        {
            this.eventService = eventService;
        }

        [HttpGet]
        public IActionResult GetEventByCityAndCategoryAndName()
        {
            return this.View();
        }

        public IActionResult GetSearchedEvents(EventSearchModel model, int id = 1)
        {
            var result = new ListEventViewModel()
            {
                Events = this.eventService.GetSearchedEvents<EventViewModel>(model),
                PageNumber = id,
                ItemsPerPage = GlobalConstants.ItemsPerPage,
            };
            result.EventsCount = result.Events.Count();
            return this.View(result);
        }
    }
}
