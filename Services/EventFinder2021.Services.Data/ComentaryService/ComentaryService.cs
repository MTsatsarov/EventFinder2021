namespace EventFinder2021.Services.Data.ComentaryService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EventFinder2021.Data;
    using EventFinder2021.Data.Common.Repositories;
    using EventFinder2021.Data.Models;
    using EventFinder2021.Services.Data.DislikeService;
    using EventFinder2021.Services.Data.LikeService;
    using EventFinder2021.Services.Models;
    using EventFinder2021.Web.ViewModels.ComentaryModels;

    public class ComentaryService : IComentaryService
    {
        private readonly IDeletableEntityRepository<Comentary> comentaryRepository;
        private readonly ApplicationDbContext db;
        private readonly IDeletableEntityRepository<Event> eventRepostiroy;
        private readonly ILikeService likeService;
        private readonly IDislikeService dislikeService;

        public ComentaryService(ApplicationDbContext db, IDeletableEntityRepository<Event> eventRepostiroy, ILikeService likeService, IDislikeService dislikeService)
        {
            this.db = db;
            this.eventRepostiroy = eventRepostiroy;
            this.likeService = likeService;
            this.dislikeService = dislikeService;
        }

        public IEnumerable<ComentaryViewModel> GetAllEventComentaries(int eventId)
        {
            List<ComentaryViewModel> comentaries = new List<ComentaryViewModel>();
            var currComentaries = this.db.Comentaries.Where(x => x.EventId == eventId).ToList();
            foreach (var comentary in currComentaries)
            {
                var currComentary = new ComentaryViewModel()
                {
                    UserName = comentary.User.UserName,
                    Content = comentary.Content,
                    EventName = comentary.Event.Name,
                    ComentaryId = comentary.Id,
                };
                var comentaryLikeDislikeCount = this.likeService.GetComentaryLikesAndDislikes(comentary.Id);
                currComentary.LikesCount = comentaryLikeDislikeCount.ComentaryLikeCount;
                currComentary.DislikesCount = comentaryLikeDislikeCount.ComentaryDislikeCount;

                foreach (var reply in comentary.Replies)
                {
                    var currReply = new ReplyViewModel()
                    {
                        Content = reply.Content,
                        UserName = reply.User.UserName,
                        ReplyId = reply.Id,
                        ReplyLikesCount = reply.Likes.Count,
                        ReplyDislikesCount = reply.Dislikes.Count,
                    };
                    currComentary.Replies.Add(currReply);
                }

                comentaries.Add(currComentary);
            }

            return comentaries;
        }

        public int GetComentaryCount(int eventId)
        {
            var currEvent = this.eventRepostiroy.All().Where(x => x.Id == eventId).FirstOrDefault();

            if (currEvent == null)
            {
                throw new ArgumentNullException("No event was found");
            }

            return currEvent.Comentaries.Count();
        }

        public async Task WriteComentary(RePostComentaryModel model)
        {
            var currComentary = new Comentary()
            {
                Content = model.Content,
                EventId = model.EventId,
                UserId = model.UserId,
            };
            await this.db.Comentaries.AddAsync(currComentary);
            await this.db.SaveChangesAsync();
        }
    }
}
