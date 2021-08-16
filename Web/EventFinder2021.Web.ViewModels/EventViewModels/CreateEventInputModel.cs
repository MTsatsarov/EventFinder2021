namespace EventFinder2021.Web.ViewModels.EventViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using EventFinder2021.Data.Models;
    using EventFinder2021.Data.Models.Enums;
    using EventFinder2021.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class CreateEventInputModel
    {
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Name must be betweeen 3 and 30 characters.")]
        public string Name { get; set; }

        [Required]
        public string CreatedByuser { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "Description must be at least 10 characters long.")]
        public string Description { get; set; }

        [Required]
        [EnumDataType(typeof(Category))]
        public Category Category { get; set; }

        [Required]
        public IFormFile Image { get; set; }

        [Required]
        [EnumDataType(typeof(City))]
        public City City { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
