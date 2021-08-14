namespace EventFinder2021.Web.ViewModels.ComentaryModels
{
    using System.Collections.Generic;

    using AutoMapper;
    using EventFinder2021.Data.Models;
    using EventFinder2021.Services.Mapping;
    using Newtonsoft.Json;

    public class ComentaryViewModel : IMapFrom<Comentary>, IMapTo<Comentary>, IHaveCustomMappings
    {
        public ComentaryViewModel()
        {
            this.Replies = new List<ReplyViewModel>();
        }

        [JsonProperty("comentaryId")]
        public int ComentaryId { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("eventName")]
        public string EventName { get; set; }

        [JsonProperty("likesCount")]
        public int LikesCount { get; set; }

        [JsonProperty("dislikesCount")]
        public int DislikesCount { get; set; }

        [JsonProperty("replies")]
        public ICollection<ReplyViewModel> Replies { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Comentary, ComentaryViewModel>()
                .ForMember(x => x.ComentaryId, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.User.UserName));
        }
    }
}
