namespace EventFinder2021.Data.Models
{
    using System;

    using EventFinder2021.Data.Common.Models;
    using EventFinder2021.Data.Models.Enums;

    public class Event : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public string ImageId { get; set; }

        public Image Image { get; set; }

        public Category Type { get; set; }

        public City? City { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
