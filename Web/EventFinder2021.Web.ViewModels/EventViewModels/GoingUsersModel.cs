namespace EventFinder2021.Web.ViewModels.EventViewModels
{
    using Newtonsoft.Json;

    public class GoingUsersModel
    {
        [JsonProperty(PropertyName = "eventid")]
        public string EventId { get; set; }
    }
}
