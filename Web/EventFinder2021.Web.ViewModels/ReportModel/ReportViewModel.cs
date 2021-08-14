namespace EventFinder2021.Web.ViewModels.ReportModel
{
    public abstract class ReportViewModel
    {
        public string ReporterUserUsername { get; set; }

        public string ReportedUserUsername { get; set; }

        public string Reason { get; set; }
    }
}
