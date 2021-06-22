namespace EventFinder2021.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using EventFinder2021.Data.Common.Models;

    public class Like : BaseDeletableModel<int>
    {
        public Like()
        {
            this.Users = new HashSet<ApplicationUser>();
        }

        public int? ComentaryId { get; set; }

        public virtual Comentary Comentary { get; set; }

        public int? ReplyId { get; set; }

        public virtual Reply Reply { get; set; }

        [InverseProperty("Likes")]
        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
