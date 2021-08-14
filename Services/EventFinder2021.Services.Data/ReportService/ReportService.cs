namespace EventFinder2021.Services.Data.ReportService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EventFinder2021.Data;
    using EventFinder2021.Data.Models;
    using EventFinder2021.Services.Mapping;
    using EventFinder2021.Web.ViewModels.ReportModel;

    public class ReportService : IReportService
    {
        private readonly ApplicationDbContext db;

        public ReportService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void CloseReport(int id)
        {
            var reportToDelete = this.db.Reports.FirstOrDefault(x => x.Id == id);
            if (reportToDelete == null)
            {
                throw new ArgumentException("No report with this id found");
            }

            reportToDelete.IsDeleted = true;
        }

        public async Task CreateReport(ReportInputModel model)
        {
            var input = new Report()
            {
                CommentaryId = model.CommentaryId,
                ReportedUserId = model.ReportedUserId,
                ReporterUserId = model.ReporterUserId,
                EventId = model.EventId,
                Reason = model.Reason,
            };
            await this.db.Reports.AddAsync(input);
            await this.db.SaveChangesAsync();
        }

        public IEnumerable<T> GetCommentaryReports<T>() => this.db.Reports.Where(x => x.CommentaryId != null).To<T>();

        public IEnumerable<T> GetEventReports<T>()
        {
           return this.db.Reports.Where(x => x.EventId != null).To<T>();
        }
    }
}
