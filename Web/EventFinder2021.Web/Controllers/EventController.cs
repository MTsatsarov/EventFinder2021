namespace EventFinder2021.Web.Controllers
{
    using System.Threading.Tasks;

    using EventFinder2021.Common;
    using EventFinder2021.Data.Models;
    using EventFinder2021.Services.Data.EventService;
    using EventFinder2021.Web.ViewModels.EventViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class EventController : Controller
    {
        private readonly IWebHostEnvironment environment;
        private readonly IEventService eventService;
        private readonly UserManager<ApplicationUser> userManager;

        public EventController(IWebHostEnvironment environment, IEventService eventService, UserManager<ApplicationUser> userManager)
        {
            this.environment = environment;
            this.eventService = eventService;
            this.userManager = userManager;
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

        public async Task<IActionResult> MyEvents(int id = 1)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userId = user.Id.ToString();
            var viewModel = new ListEventViewModel()
            {
                ItemsPerPage = GlobalConstants.ItemsPerPage,
                PageNumber = id,
                Events = this.eventService.GetEventsByUser(userId),
                EventsCount = this.eventService.GetCount(),
            };
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult GoingToEvent([FromBody] GoingUsers model)
        {

            var userId = model.UserId;
            var eventId = model.EventId;
            var goingUsersCount = this.eventService.AddGoingUser(userId, eventId).ToString();
            return this.Json(new { count = $"{goingUsersCount}" });
        }

        [HttpPost]
        [Authorize]
        public IActionResult NotGoingToEvent([FromBody] GoingUsers model)
        {
            var userId = model.UserId;
            var eventId = model.EventId;
            var notGoingUsersCount = this.eventService.AddNotGoingUserAsync(userId, eventId);
            return this.Json(new { count = $"{notGoingUsersCount}" });
        }
    }
}
