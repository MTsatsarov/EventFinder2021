namespace EventFinder2021.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using EventFinder2021.Data;
    using EventFinder2021.Data.Models;
    using EventFinder2021.Services.Data.ReportService;
    using EventFinder2021.Services.Mapping;
    using EventFinder2021.Web.ViewModels;
    using EventFinder2021.Web.ViewModels.ReportModel;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class ReportServiceTests
    {
        private ApplicationUser user;
        private Event inputModel;

        public ReportServiceTests()
        {
            this.user = new ApplicationUser()
            {
                UserName = "User",
            };
            this.inputModel = new Event()
            {
                Category = (EventFinder2021.Data.Models.Enums.Category)1,
                City = (EventFinder2021.Data.Models.Enums.City)1,
                Description = "aaaaaaaaaaaaaaaa",
                User = this.user,
                Name = "Name",
                Date = DateTime.Now,
            };
        }

        [Fact]
        public async Task AssertClearReportDoNothingIfIsAlreadyDeleted()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("AssertClearReportDoNothingIfDeleted");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            await dbContext.Events.AddAsync(this.inputModel);
            await dbContext.Users.AddAsync(this.user);
            await dbContext.SaveChangesAsync();
            var reportService = new ReportService(dbContext);
            var model = new ReportInputModel()
            {
                CommentaryId = null,
                EventId = 1,
                Reason = "adasdasdasd",
                ReportedUserId = this.user.Id,
                ReporterUserId = this.user.Id,
            };
            await reportService.CreateReportAsync(model);
            reportService.ClearReport(1);
            Assert.Throws<ArgumentException>(() => reportService.ClearReport(1)).Message.Contains("Report not found");
        }

        [Fact]
        public async Task AssertClearReportSetsReportToDeleted()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("AssertClearReportDoNothingIfDeleted");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            await dbContext.Events.AddAsync(this.inputModel);
            await dbContext.Users.AddAsync(this.user);
            await dbContext.SaveChangesAsync();
            var reportService = new ReportService(dbContext);
            var model = new ReportInputModel()
            {
                CommentaryId = null,
                EventId = 1,
                Reason = "adasdasdasd",
                ReportedUserId = this.user.Id,
                ReporterUserId = this.user.Id,
            };

            await reportService.CreateReportAsync(model);
            await reportService.CreateReportAsync(model);
            await reportService.CreateReportAsync(model);
            reportService.ClearReport(2);
            var reportCount = await dbContext.Reports.CountAsync();
            Assert.Equal(2, reportCount);
        }

        [Fact]
        public async Task AssertCloseReportThrowExceptionIfIdInvalid()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("AssertClearReportDoNothingIfDeleted");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            await dbContext.Events.AddAsync(this.inputModel);
            await dbContext.Users.AddAsync(this.user);
            await dbContext.SaveChangesAsync();
            var reportService = new ReportService(dbContext);
            var model = new ReportInputModel()
            {
                CommentaryId = null,
                EventId = 1,
                Reason = "adasdasdasd",
                ReportedUserId = this.user.Id,
                ReporterUserId = this.user.Id,
            };

            await reportService.CreateReportAsync(model);

            Assert.Throws<ArgumentException>(() => reportService.CloseReport(2424)).Message.Contains("No report with this id found");
        }

        [Fact]
        public async Task AssertCloseReportDeletesReportIfEventNull()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("AssertCloseReportDeletesReportIfEventNulllll");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            await dbContext.Events.AddAsync(this.inputModel);
            await dbContext.Users.AddAsync(this.user);
            await dbContext.SaveChangesAsync();
            var reportService = new ReportService(dbContext);
            var model = new Report()
            {
                CommentaryId = null,
                EventId = 1,
                Reason = "adasdasdasd",
                ReportedUserId = this.user.Id,
                ReporterUserId = this.user.Id,
            };
            await dbContext.Reports.AddAsync(model);
            await dbContext.SaveChangesAsync();
            reportService.CloseReport(1);
            var count = await dbContext.Reports.CountAsync();

            Assert.Equal(0, count);
            Assert.Equal(0, await dbContext.Events.CountAsync());
        }

        [Fact]
        public async Task AssertGetEventReportReturnsEventCountOfReports()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("AssertCloseReportDeletesReportIfEventNul11l");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            await dbContext.Events.AddAsync(this.inputModel);
            await dbContext.Users.AddAsync(this.user);
            await dbContext.SaveChangesAsync();
            var reportService = new ReportService(dbContext);
            var model = new Report()
            {
                CommentaryId = null,
                EventId = 1,
                Reason = "adasdasdasd",
                ReportedUserId = this.user.Id,
                ReporterUserId = this.user.Id,
            };
            var model2 = new Report()
            {
                CommentaryId = null,
                EventId = 1,
                Reason = "adasdasdasd",
                ReportedUserId = this.user.Id,
                ReporterUserId = this.user.Id,
            };

            await dbContext.Reports.AddAsync(model);
            await dbContext.Reports.AddAsync(model2);
            dbContext.SaveChanges();

            var count = reportService.GetEventReports<EventReportViewModel>().Count();
            Assert.Equal(2, count);
        }

    }
}
