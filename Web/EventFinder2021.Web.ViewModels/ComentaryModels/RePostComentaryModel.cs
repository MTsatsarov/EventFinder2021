namespace EventFinder2021.Web.ViewModels.ComentaryModels
{
    using Newtonsoft.Json;

    public class RePostComentaryModel
    {
        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("eventId")]
        public int EventId { get; set; }

        public string UserId { get; set; }
    }
}
