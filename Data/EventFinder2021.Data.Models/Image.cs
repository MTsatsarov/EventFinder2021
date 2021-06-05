namespace EventFinder2021.Data.Models
{
    using System;

    using EventFinder2021.Data.Common.Models;

    public class Image : BaseDeletableModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Extension { get; set; }

        public int EventId { get; set; }

        public virtual Event Event { get; set; }

        public string RemoteUrl { get; set; }

        public string AddedByUserId { get; set; }

        public virtual ApplicationUser AddedByUser { get; set; }
    }
}
