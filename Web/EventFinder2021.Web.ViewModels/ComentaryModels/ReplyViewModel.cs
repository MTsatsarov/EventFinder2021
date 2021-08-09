namespace EventFinder2021.Web.ViewModels.ComentaryModels
{
    using System.Linq;

    using AutoMapper;
    using EventFinder2021.Data.Models;
    using EventFinder2021.Services.Mapping;
    using Newtonsoft.Json;

    public class ReplyViewModel : IMapFrom<Reply>, IMapTo<Reply>, IHaveCustomMappings
    {
        [JsonProperty("comentaryId")]
        public int ComentaryId { get; set; }

        [JsonProperty("replyId")]
        public int ReplyId { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("replyLikesCount")]
        public int ReplyLikesCount { get; set; }

        [JsonProperty("replyDislikesCount")]
        public int ReplyDislikesCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Reply, ReplyViewModel>()
                .ForMember(x => x.ReplyId, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.User.UserName))
                .ForMember(x => x.ReplyLikesCount, opt => opt.MapFrom(x => x.Likes.Count()))
                .ForMember(x => x.ReplyDislikesCount, opt => opt.MapFrom(x => x.Dislikes.Count()));
        }
    }
}
