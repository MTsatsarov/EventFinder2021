﻿namespace EventFinder2021.Data.Models
{
    using System;
    using System.Collections.Generic;

    using EventFinder2021.Data.Common.Models;
    using EventFinder2021.Data.Models.Enums;

    public class Event : BaseDeletableModel<int>
    {
        public Event()
        {
            this.GoingUsers = new HashSet<ApplicationUser>();
            this.NotGoingUsers = new HashSet<ApplicationUser>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public string ImageId { get; set; }

        public virtual Image Image { get; set; }

        public Category Type { get; set; }

        public City? City { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public ICollection<ApplicationUser> GoingUsers { get; set; }

        public ICollection<ApplicationUser> NotGoingUsers { get; set; }
    }
}
