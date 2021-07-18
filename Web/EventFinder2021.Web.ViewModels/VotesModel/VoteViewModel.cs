namespace EventFinder2021.Web.ViewModels.VotesModel
{
    using Newtonsoft.Json;

    public class VoteViewModel
    {
        [JsonProperty(PropertyName = "averageVoteValue")]
        public double AverageVoteValue { get; set; }
    }
}
