namespace EventFinder2021.Web.ViewModels.EventViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using EventFinder2021.Data.Models;
    using EventFinder2021.Data.Models.Enums;
    using EventFinder2021.Services.Mapping;

    public class EventViewModel : IMapFrom<Event>, IMapTo<Event>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [MinLength(10)]
        public string Description { get; set; }

        [Required]
        [EnumDataType(typeof(City))]
        public City? City { get; set; }

        [Required]
        [EnumDataType(typeof(Category))]
        public Category Category { get; set; }

        public string ImageUrl { get; set; }

        public DateTime Date { get; set; }

        public string CreatorId { get; set; }

        public double VotesAverageGrade { get; set; }

        public int GoingUsers { get; set; }
        public int NotGoingUsers { get; set; }
    }
}
