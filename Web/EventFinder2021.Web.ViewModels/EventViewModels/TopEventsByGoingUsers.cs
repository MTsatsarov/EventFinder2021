namespace EventFinder2021.Web.ViewModels.EventViewModels
{
    using Newtonsoft.Json;

    public class TopEventsByGoingUsers : TopEvents
    {
        [JsonProperty("count")]
        public int GoingUsersCount { get; set; }
    }
}
