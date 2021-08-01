    namespace EventFinder2021.Web.ViewModels.ComentaryModels
{
    using Newtonsoft.Json;

    public class ReplyViewModel
    {
        [JsonProperty("comentaryId")]
        public int ComentaryId { get; set; }

        [JsonProperty("replyId")]
        public int ReplyId { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("replyLikesCount")]
        public int ReplyLikesCount { get; set; }

        [JsonProperty("replyDislikesCount")]
        public int ReplyDislikesCount { get; set; }
    }
}
