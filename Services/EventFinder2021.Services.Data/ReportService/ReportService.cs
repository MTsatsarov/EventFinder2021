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

        public void ClearReport(int id)
        {
            var report = this.db.Reports.Where(x => x.Id == id).FirstOrDefault();
            if (report == null)
            {
                return;
            }

            report.IsDeleted = true;
            this.db.Reports.Update(report);
            this.db.SaveChanges();
        }

        public void CloseReport(int id)
        {
            var reportToDelete = this.db.Reports.FirstOrDefault(x => x.EventId == id);
            if (reportToDelete == null)
            {
                throw new ArgumentException("No report with this id found");
            }

            var eventToRemove = this.db.Events.Where(x => x.Id == id).FirstOrDefault();
            if (eventToRemove == null)
            {
                reportToDelete.IsDeleted = true;
                this.db.SaveChanges();
                return;
            }

            eventToRemove.IsDeleted = true;
            this.db.Events.Update(eventToRemove);
            reportToDelete.IsDeleted = true;
            this.db.SaveChanges();
        }

        public async Task CreateReportAsync(ReportInputModel model)
        {
            var reportedUserName = this.db.Users.First(x => x.Id == model.ReportedUserId).UserName;
            var reporterUserUsername = this.db.Users.First(x => x.Id == model.ReporterUserId).UserName;
            var input = new Report()
            {
                CommentaryId = model.CommentaryId,
                ReportedUserId = reportedUserName,
                ReporterUserId = reporterUserUsername,
                EventId = model.EventId,
                Reason = model.Reason,
            };
            await this.db.Reports.AddAsync(input);
            await this.db.SaveChangesAsync();
        }

        public IEnumerable<T> GetCommentaryReports<T>() => this.db.Reports.Where(x => x.CommentaryId != null).To<T>();

        public IEnumerable<T> GetEventReports<T>()
        {
            return this.db.Reports.Where(x => x.EventId != null && x.Event.IsDeleted != true && x.IsDeleted == false).To<T>();
        }
    }
}
