namespace EventFinder2021.Web.ViewModels.ComentaryModels
{
    using Newtonsoft.Json;

    public class GetComentaryModel
    {
        [JsonProperty("eventId")]
        public string EventId { get; set; }
    }
}
