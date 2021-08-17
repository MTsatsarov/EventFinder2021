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
        [Required]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Reason must be at between 5 and 100 characters.")]
        public string Reason { get; set; }
    }
}
