namespace EventFinder2021.Web.ViewModels.EventViewModels
{
    using System.Linq;

    using AutoMapper;
    using EventFinder2021.Data.Models;
    using EventFinder2021.Services.Mapping;
    using Newtonsoft.Json;

    public class TopEventsByGoingUsers : TopEvents, IMapFrom<Event>, IMapTo<Event>, IHaveCustomMappings
    {
        [JsonProperty("count")]
        public int GoingUsersCount { get; set; }

        public void CreateMappings(IProfileExpression configuration) => configuration.CreateMap<Event, TopEventsByGoingUsers>()
               .ForMember(x => x.GoingUsersCount, opt => opt.MapFrom(x => x.GoingUsers.Users.Count()))
               .ForMember(x => x.CreatorName, opt => opt.MapFrom(x =>
               x.User.UserName));
    }
}
