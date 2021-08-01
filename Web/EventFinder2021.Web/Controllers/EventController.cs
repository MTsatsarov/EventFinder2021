namespace EventFinder2021.Web.Controllers
{
    using System.Security.Claims;
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
        public IActionResult Edit(int id)
        {
            var model = this.eventService.GetEventById(id);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userId != model.CreatorId)
            {
                return this.Redirect("/");
            }

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EventViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.eventService.UpdateInfo(model);
            return this.Redirect("/Event/MyEvents");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateEvent(CreateEventInputModel model)
        {
            // if (!this.ModelState.IsValid)
            // {
            //   return this.View(this.ModelState.ErrorCount );
            // }
            var imagePath = this.environment.WebRootPath;
            model.CreatedByuser = this.User.Identity.Name;
            await this.eventService.CreateEventAsync(model, imagePath);
            return this.Redirect("/");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var currEvent = this.eventService.GetEventById(id);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (currEvent.CreatorId == userId)
            {
                await this.eventService.DeleteEventAsync(id);
            }

            return this.Redirect("/");
        }

        public IActionResult EventView(int id)
        {
            var model = this.eventService.GetEventById(id);
            return this.View(model);
        }

        [Authorize]
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
        [IgnoreAntiforgeryToken]
        public IActionResult GoingToEvent([FromBody] GoingUsersModel model)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var eventId = int.Parse(model.EventId);
            var eventUsers = this.eventService.AddGoingUser(userId, eventId);
            return this.Json(new GoingNotGoingViewModel()
            {
                GoingUsersCount = eventUsers.GoingUsersCount,
                NotGoingUsersCount = eventUsers.NotGoingUsersCount,
            });
        }

        [HttpPost]
        [Authorize]
        [IgnoreAntiforgeryToken]
        public IActionResult NotGoingToEvent([FromBody] GoingUsersModel model)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var eventId = int.Parse(model.EventId);
            var eventUsers = this.eventService.AddNotGoingUser(userId, eventId);
            return this.Json(new GoingNotGoingViewModel()
            {
                GoingUsersCount = eventUsers.GoingUsersCount,
                NotGoingUsersCount = eventUsers.NotGoingUsersCount,
            });
        }
    }
}
