namespace EventFinder2021.Services.Data.ComentaryService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EventFinder2021.Data;
    using EventFinder2021.Data.Common.Repositories;
    using EventFinder2021.Data.Models;
    using EventFinder2021.Services.Data.LikeService;
    using EventFinder2021.Services.Models;
    using EventFinder2021.Web.ViewModels.ComentaryModels;

    public class ComentaryService : IComentaryService
    {
        private readonly IDeletableEntityRepository<Comentary> comentaryRepository;
        private readonly IDeletableEntityRepository<Event> eventRepostiroy;
        private readonly ILikeService likeService;

        public ComentaryService(IDeletableEntityRepository<Comentary> comentaryRepository, IDeletableEntityRepository<Event> eventRepostiroy, ILikeService likeService)
        {
            this.comentaryRepository = comentaryRepository;
            this.eventRepostiroy = eventRepostiroy;
            this.likeService = likeService;
        }

        public IEnumerable<ComentaryViewModel> GetAllEventComentaries(int eventId)
        {
            List<ComentaryViewModel> comentaries = new List<ComentaryViewModel>();
            var currComentaries = this.comentaryRepository.All().Where(x => x.EventId == eventId).ToList();
            foreach (var comentary in currComentaries)
            {
                var currComentary = new ComentaryViewModel()
                {
                    UserName = comentary.User.UserName,
                    Content = comentary.Content,
                    EventName = comentary.Event.Name,
                    ComentaryId = comentary.Id,
                    LikesCount = this.likeService.GetComentaryLikes(comentary.Id),
                };

                foreach (var reply in comentary.Replies)
                {
                    var currReply = new ReplyViewModel()
                    {
                        Content = reply.Content,
                        UserName = reply.User.UserName,
                        ReplyId = reply.Id,
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

        public async Task WriteComentary(PostComentaryModel model)
        {
            var currComentary = new Comentary()
            {
                Content = model.Content,
                EventId = model.EventId,
                UserId = model.UserId,
            };
            await this.comentaryRepository.AddAsync(currComentary);
            await this.comentaryRepository.SaveChangesAsync();
        }
    }
}
