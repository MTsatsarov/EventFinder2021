namespace EventFinder2021.Web.ViewModels.EventViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using AutoMapper;
    using EventFinder2021.Common;
    using EventFinder2021.Data.Models;
    using EventFinder2021.Data.Models.Enums;
    using EventFinder2021.Services.Mapping;

    public class EventViewModel : IMapTo<Event>, IMapFrom<Event>, IHaveCustomMappings
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

        public int GoingUsersCount { get; set; }

        public int NotGoingUsersCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Event, EventViewModel>()
              .ForMember(x => x.ImageUrl, opt =>
                  opt.MapFrom(
                      x =>
                    GlobalConstants.ImageUrl + x.Image.Id + "." + x.Image.Extension))
               .ForMember(x => x.VotesAverageGrade, opt =>
               opt.MapFrom(x => x.Votes.Count() > 0 ?
               x.Votes.Average(x => x.Grade) : 0))

                .ForMember(x => x.GoingUsersCount, opt =>
                   opt.MapFrom(x =>
                       x.GoingUsers.Users.Count()))

                .ForMember(x => x.NotGoingUsersCount, opt =>
                   opt.MapFrom(x =>
                       x.NotGoingUsers.Users.Count()))
                 .ForMember(x => x.CreatorId, opt =>
                   opt.MapFrom(x =>
                       x.UserId));


        }
    }
}
