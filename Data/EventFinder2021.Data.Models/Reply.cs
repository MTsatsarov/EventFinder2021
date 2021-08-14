namespace EventFinder2021.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using EventFinder2021.Data.Common.Models;

    public class Reply : BaseDeletableModel<int>
    {
        public Reply()
        {
            this.Likes = new List<Like>();
            this.Dislikes = new List<Dislike>();
        }

        public string Content { get; set; }

        public string UserId { get; set; }

        public int EventId { get; set; }

        public virtual Event Event { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int ComentaryId { get; set; }

        [InverseProperty("Replies")]
        public virtual Comentary Comentary { get; set; }

        [InverseProperty("Reply")]
        public virtual ICollection<Like> Likes { get; set; }

        [InverseProperty("Reply")]
        public virtual ICollection<Dislike> Dislikes { get; set; }
    }
}
