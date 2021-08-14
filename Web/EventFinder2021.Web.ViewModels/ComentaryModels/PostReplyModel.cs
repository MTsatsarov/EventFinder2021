namespace EventFinder2021.Web.ViewModels.ComentaryModels
{
    using Newtonsoft.Json;

    public class PostReplyModel
    {
        [JsonProperty("content")]
        public string Content { get; set; }

        public string UserId { get; set; }

        [JsonProperty("comentaryId")]
        public int ComentaryId { get; set; }

        [JsonProperty("eventId")]
        public int EventId { get; set; }
    }
}
