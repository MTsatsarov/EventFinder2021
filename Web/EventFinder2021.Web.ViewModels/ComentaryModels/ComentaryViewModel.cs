namespace EventFinder2021.Web.ViewModels.ComentaryModels
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public class ComentaryViewModel
    {
        public ComentaryViewModel()
        {
            this.Replies = new List<ReplyViewModel>();
        }

        [JsonProperty("comentaryId")]
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
