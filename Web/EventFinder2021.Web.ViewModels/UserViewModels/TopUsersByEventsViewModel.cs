namespace EventFinder2021.Web.ViewModels.UserViewModels
{
    using Newtonsoft.Json;

    public class TopUsersByEventsViewModel
    {
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("eventCount")]
        public int EventCount { get; set; }
    }
}
