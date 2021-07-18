namespace EventFinder2021.Web.ViewModels.LikeDislikeViewModel
{
    using Newtonsoft.Json;

    public class LikeDislikeViewModel
    {
        [JsonProperty("comentaryLikeCount")]
        public int ComentaryLikeCount { get; set; }

        [JsonProperty("comentaryDislikeCount")]
        public int ComentaryDislikeCount { get; set; }
    }
}
