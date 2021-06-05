namespace EventFinder2021.Web.ViewModels.EventViewModels
{
    using Newtonsoft.Json;

    public class GoingUsersModel
    {
        [JsonProperty(PropertyName = "userid")]
        public string UserId { get; set; }

        [JsonProperty(PropertyName = "eventid")]
        public int EventId { get; set; }
    }
}
