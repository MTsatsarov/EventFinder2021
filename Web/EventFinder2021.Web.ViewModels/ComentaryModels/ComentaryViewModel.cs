namespace EventFinder2021.Web.ViewModels.ComentaryModels
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class ComentaryViewModel
    {
        public ComentaryViewModel()
        {
            this.Replies = new List<ReplyViewModel>();
        }

        public int ComentaryId { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("eventName")]
        public string EventName { get; set; }

        [JsonProperty("likesCount")]
        public int LikesCount { get; set; }

        [JsonProperty("dislikesCount")]
        public int DislikesCount { get; set; }

        [JsonProperty("replies")]
        public ICollection<ReplyViewModel> Replies { get; set; }
    }
}
