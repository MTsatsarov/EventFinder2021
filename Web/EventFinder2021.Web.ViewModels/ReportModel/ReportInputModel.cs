namespace EventFinder2021.Web.ViewModels.ReportModel
{
    using System.ComponentModel.DataAnnotations;

    using EventFinder2021.Data.Models;
    using EventFinder2021.Services.Mapping;

    public class ReportInputModel
    {
        [Required]
        public string ReportedUserId { get; set; }

        [Required]
        public string ReporterUserId { get; set; }

        public int? EventId { get; set; }

        public int? CommentaryId { get; set; }

        [MinLength(5, ErrorMessage = "Reason must be at least 5 characters.")]
        public string Reason { get; set; }
    }
}
