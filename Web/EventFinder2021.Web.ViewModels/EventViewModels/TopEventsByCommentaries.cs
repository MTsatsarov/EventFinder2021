namespace EventFinder2021.Web.ViewModels.EventViewModels
{
    using System.Linq;

    using AutoMapper;
    using EventFinder2021.Data.Models;
    using EventFinder2021.Services.Mapping;
    using Newtonsoft.Json;

    public class TopEventsByCommentaries : TopEvents, IMapFrom<Event>, IHaveCustomMappings
    {
        [JsonProperty("count")]
        public int CommentaryCount { get; set; }

        public void CreateMappings(IProfileExpression configuration) => configuration.CreateMap<Event, TopEventsByCommentaries>()
                .ForMember(x => x.CommentaryCount, opt => opt.MapFrom(x => x.Comentaries.Count()))
                .ForMember(x => x.CreatorName, opt => opt.MapFrom(x =>
                x.User.UserName));
    }
}
