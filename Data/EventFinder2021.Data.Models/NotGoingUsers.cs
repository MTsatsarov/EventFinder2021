namespace EventFinder2021.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using EventFinder2021.Data.Common.Models;

    public class NotGoingUsers : BaseDeletableModel<int>
    {
        public NotGoingUsers()
        {
            this.Users = new HashSet<ApplicationUser>();
        }

        public virtual ICollection<ApplicationUser> Users { get; set; }

        public int EventId { get; set; }

        public virtual Event Event { get; set; }
    }
}
