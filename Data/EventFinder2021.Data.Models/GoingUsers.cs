namespace EventFinder2021.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using EventFinder2021.Data.Common.Models;

    public class GoingUsers : BaseDeletableModel<int>
    {
        public GoingUsers()
        {
            this.Users = new HashSet<ApplicationUser>();
        }

        public virtual ICollection<ApplicationUser> Users { get; set; }

        public int EventId { get; set; }

        public virtual Event Event { get; set; }
    }
}
