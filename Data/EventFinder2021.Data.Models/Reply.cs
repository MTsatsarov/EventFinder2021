namespace EventFinder2021.Data.Models
{
    using EventFinder2021.Data.Common.Models;

    public class Reply : BaseDeletableModel<int>
    {
        public string Content { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int ComentaryId { get; set; }

        public virtual Comentary Comentary { get; set; }
    }
}
