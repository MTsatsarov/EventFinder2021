namespace EventFinder2021.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using EventFinder2021.Data.Common.Models;

    public class Comentary : BaseDeletableModel<int>
    {
        public Comentary()
        {
            this.Replies = new HashSet<Reply>();
        }

        public string Content { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int EventId { get; set; }

        [ForeignKey("EventId")]
        public virtual Event Event { get; set; }

        [InverseProperty("Comentary")]
        public virtual ICollection<Reply> Replies { get; set; }
    }
}
