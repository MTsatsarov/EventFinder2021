namespace EventFinder2021.Web.Controllers
{
    using EventFinder2021.Services.Data.EventService;
    using EventFinder2021.Web.ViewModels.EventViewModels;
    using Microsoft.AspNetCore.Mvc;

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

        public IActionResult GetSearchedEvents(EventSearchModel model)
        {
            var searchedEvents = this.eventService.GetSearchedEvents(model);
            return this.View(searchedEvents);
        }
    }
}
