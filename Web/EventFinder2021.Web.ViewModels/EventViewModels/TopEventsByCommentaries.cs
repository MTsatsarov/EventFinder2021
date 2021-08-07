namespace EventFinder2021.Web.ViewModels.EventViewModels
{
    using Newtonsoft.Json;

    public class TopEventsByCommentaries : TopEvents
    {
        [JsonProperty("count")]
        public int CommentaryCount { get; set; }
    }
}
