namespace EventFinder2021.Web.ViewModels.StatisticViewModels
{
    using System.Collections.Generic;

    using EventFinder2021.Web.ViewModels.EventViewModels;
    using EventFinder2021.Web.ViewModels.UserViewModels;
    using Newtonsoft.Json;

    public class StatistiViewModel
    {
        [JsonProperty(PropertyName = "mostCommentedEvents")]
        public IEnumerable<TopEventsByCommentaries> MostCommentedEvents { get; set; }

        [JsonProperty(PropertyName = "mostVisitedEvents")]
        public IEnumerable<TopEventsByGoingUsers> MostVisitedEvents { get; set; }

        [JsonProperty(PropertyName = "usersCount")]
        public int UsersCount { get; set; }

        [JsonProperty(PropertyName = "topUsers")]
        public IEnumerable<TopUsersByEventsViewModel> TopUsers { get; set; }
    }
}
