namespace EventFinder2021.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using EventFinder2021.Data.Common.Models;
    using EventFinder2021.Data.Models.Enums;

    public class Event : BaseDeletableModel<int>
    {
        public Event()
        {
            this.Comentaries = new HashSet<Comentary>();
            this.Votes = new HashSet<Vote>();
            this.NotGoingUsers = new NotGoingUsers();
            this.GoingUsers = new GoingUsers();
            this.Replies = new HashSet<Reply>();
            this.Reports = new HashSet<Report>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public string ImageId { get; set; }

        public virtual Image Image { get; set; }

        public Category Category { get; set; }

        public City? City { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int? GoingUsersId { get; set; }

        public virtual GoingUsers GoingUsers { get; set; }

        public int? NotGoingUsersId { get; set; }

        public virtual NotGoingUsers NotGoingUsers { get; set; }

        public int ComentaryId { get; set; }

        [InverseProperty("Event")]
        public virtual ICollection<Comentary> Comentaries { get; set; }

        [InverseProperty("Event")]
        public virtual ICollection<Vote> Votes { get; set; }

        [InverseProperty("Event")]
        public virtual ICollection<Reply> Replies { get; set; }

        [InverseProperty("Event")]
        public virtual ICollection<Report> Reports { get; set; }
    }
}
