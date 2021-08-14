namespace EventFinder2021.Web.ViewModels.ReportModel
{
    using System.ComponentModel.DataAnnotations;

    using EventFinder2021.Data.Models;
    using EventFinder2021.Services.Mapping;

    public class ReportInputModel : IMapFrom<Report>
    {
        [Required]
        public string ReportedUserId { get; set; }

        [Required]
        public string ReporterUserId { get; set; }

        public int? EventId { get; set; }

        public int? CommentaryId { get; set; }

        [MinLength(5)]
        public string Reason { get; set; }
    }
}
