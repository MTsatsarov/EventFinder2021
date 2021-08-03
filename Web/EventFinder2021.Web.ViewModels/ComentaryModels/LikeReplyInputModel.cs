namespace EventFinder2021.Web.ViewModels.ComentaryModels
{
    using Newtonsoft.Json;

    public class LikeReplyInputModel
    {
        [JsonProperty("replyId")]
        public string ReplyId { get; set; }
    }
}
