namespace EventFinder2021.Services.Data.ReportService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EventFinder2021.Web.ViewModels.ReportModel;

    public interface IReportService
    {
        public Task CreateReportAsync(ReportInputModel model);

        public IEnumerable<T> GetEventReports<T>();

        public IEnumerable<T> GetCommentaryReports<T>();

        public void CloseReport(int id);

        public void ClearReport(int id);
    }
}
