namespace EventFinder2021.Web.ViewModels.ReportModel
{
    using Newtonsoft.Json;

    public abstract class ReportViewModel
    {
        [JsonProperty("reporterUsername")]
        public string ReporterUserUsername { get; set; }

        [JsonProperty("reportedUsername")]
        public string ReportedUserUsername { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }
    }
}
