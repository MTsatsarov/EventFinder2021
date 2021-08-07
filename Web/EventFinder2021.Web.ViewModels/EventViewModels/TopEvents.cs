namespace EventFinder2021.Web.ViewModels
{
    using Newtonsoft.Json;

    public abstract class TopEvents
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("creatorName")]
        public string CreatorName { get; set; }
    }
}
