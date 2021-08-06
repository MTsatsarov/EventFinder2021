namespace EventFinder2021.Web.ViewModels.StatisticViewModels
{
    using System.Collections.Generic;

    using EventFinder2021.Web.ViewModels.EventViewModels;
    using EventFinder2021.Web.ViewModels.UserViewModels;

    public class StatistiViewModel
    {
        public IEnumerable<TopEventsByCommentaries> MostCommentedEvents { get; set; }

        public IEnumerable<TopEventsByGoingUsers> MostVisitedEvents { get; set; }

        public int UsersCount { get; set; }

        public IEnumerable<TopUsersByEventsViewModel> TopUsers { get; set; }
    }
}
