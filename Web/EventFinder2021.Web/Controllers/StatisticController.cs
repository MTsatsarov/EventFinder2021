namespace EventFinder2021.Web.Controllers
{
    using EventFinder2021.Services.Data.EventService;
    using EventFinder2021.Services.Data.UserService;
    using EventFinder2021.Web.ViewModels.EventViewModels;
    using EventFinder2021.Web.ViewModels.StatisticViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class StatisticController : Controller
    {
        private readonly IUserService userService;
        private readonly IEventService eventService;

        public StatisticController(IUserService userService, IEventService eventService)
        {
            this.userService = userService;
            this.eventService = eventService;
        }

        public IActionResult Index()
        {
            var topUsers = this.userService.TopTenUsers();
            var usersCount = this.userService.GetTotalCountOfUsers();
            var topEventsByComments = this.eventService.GetMostCommentedEvents<TopEventsByCommentaries>();
            var topEventsByGoingUsers = this.eventService.GetMostVisitedEvents();

            var statisticModel = new StatistiViewModel()
            {
                MostCommentedEvents = topEventsByComments,
                TopUsers = topUsers,
                MostVisitedEvents = topEventsByGoingUsers,
                UsersCount = usersCount,
            };
            return this.View(statisticModel);
        }
    }
}
