namespace EventFinder2021.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using EventFinder2021.Data.Common.Models;

    public class Report : BaseDeletableModel<int>
    {
        [Required]
        public string ReportedUserId { get; set; }

        [Required]
        public string ReporterUserId { get; set; }

        [Required]

        public string Reason { get; set; }

        public int? EventId { get; set; }

        [InverseProperty("Reports")]
        public virtual Event Event { get; set; }

        public int? CommentaryId { get; set; }

        public virtual Comentary Comentary { get; set; }
    }
}
