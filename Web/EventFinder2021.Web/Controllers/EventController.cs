namespace EventFinder2021.Web.Controllers
{
    using System.Threading.Tasks;

    using EventFinder2021.Services.Data.EventService;
    using EventFinder2021.Web.ViewModels.EventViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;

    public class EventController : Controller
    {
        private readonly IWebHostEnvironment environment;
        private readonly IEventService eventService;

        public EventController(IWebHostEnvironment environment, IEventService eventService)
        {
            this.environment = environment;
            this.eventService = eventService;
        }

        [Authorize]
        public IActionResult CreateEvent()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateEvent(CreateEventInputModel model)
        {
            // if (!this.ModelState.IsValid)
            // {
            //    return this.View(this.ModelState.ErrorCount);
            // }
            var imagePath = this.environment.WebRootPath;
            model.CreatedByuser = this.User.Identity.Name;
            await this.eventService.CreateEventAsync(model, imagePath);
            return this.Redirect("/");
        }

        public IActionResult EventView(int id)
        {
            var model = this.eventService.GetEventById(id);
            return this.View(model);
        }
    }
}
