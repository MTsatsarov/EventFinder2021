namespace EventFinder2021.Web.ViewModels.EventViewModels
{
    using Newtonsoft.Json;

    public class GoingNotGoingViewModel
    {
        [JsonProperty(PropertyName = "goingUsersCount")]
        public int GoingUsersCount { get; set; }

        [JsonProperty(PropertyName = "notGoingUsersCount")]
        public int NotGoingUsersCount { get; set; }
    }
}
