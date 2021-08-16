namespace EventFinder2021.Web.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using EventFinder2021.Services.Data.ReportService;
    using EventFinder2021.Web.ViewModels.ReportModel;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ReportController : Controller
    {
        private readonly IReportService reportService;

        public ReportController(IReportService reportService)
        {
            this.reportService = reportService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ReportEvent(ReportInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                //TODO
            }

            await this.reportService.CreateReportAsync(model);
            return this.Redirect("/");
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetEventReport()
        {
            var reports = this.reportService.GetEventReports<EventReportViewModel>().ToList();

            return this.Json(reports);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult DeleteEvent([FromBody] int id)
        {
            this.reportService.CloseReport(id);

            return this.Redirect("/");
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult ClearEvent([FromBody] int id)
        {

            this.reportService.ClearReport(id);

            return this.Json(id);
        }
    }
}
