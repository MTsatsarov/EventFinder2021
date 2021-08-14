namespace EventFinder2021.Services.Data.ReportService
{
    using EventFinder2021.Web.ViewModels.ReportModel;

   public interface IReportService
    {
        public void CreateReport(ReportInputModel model);

        public void GetEventReports(ReportViewModel model);

        public void GetCommentaryReports(ReportViewModel model);

        public void CloseReport(int id);
    }
}
