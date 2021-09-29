namespace EventFinder2021.Services.Data.ComentaryService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EventFinder2021.Data;
    using EventFinder2021.Data.Models;
    using EventFinder2021.Services.Mapping;
    using EventFinder2021.Web.ViewModels.ComentaryModels;

    public class ComentaryService : IComentaryService
    {
        private readonly ApplicationDbContext db;

        public ComentaryService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<T> GetAllEventComentaries<T>(int eventId)
        {
            var currComentaries = this.db.Comentaries.Where(x => x.EventId == eventId)
                                                     .To<T>()
                                                     .ToList();

            return currComentaries;
        }

        public int GetComentaryCount(int eventId)
        {
            var currEvent = this.db.Events.Where(x => x.Id == eventId).FirstOrDefault();

            if (currEvent == null)
            {
                throw new ArgumentException("No event were found");
            }

            return currEvent.Comentaries.Count();
        }

        public async Task WriteCommentaryAsync(RePostComentaryModel model)
        {
            var thisEvent = this.db.Events.Where(x => x.Id == model.EventId).FirstOrDefault();
            var user = this.db.Users.Where(x => x.Id == model.UserId).FirstOrDefault();
            if (user == null)
            {
                throw new ArgumentException($"No user with {model.UserId} found.");
            }

            if (thisEvent == null)
            {
                throw new ArgumentException($"No event with id {model.EventId} found.");
            }

            var currComentary = new Comentary()
            {
                Content = model.Content,
                EventId = model.EventId,
                UserId = model.UserId,
            };
            await this.db.Comentaries.AddAsync(currComentary);
            await this.db.SaveChangesAsync();
        }

       public ComentaryViewModel ReturnLastAddedComment()
        {
            var lastAddedComment = this.db.Comentaries.OrderByDescending(x => x.CreatedOn).First();
            var result = new ComentaryViewModel()
            {
                ComentaryId = lastAddedComment.Id,
                Content = lastAddedComment.Content,
                DislikesCount = lastAddedComment.Dislikes.Count(),
                LikesCount = lastAddedComment.Likes.Count(),
                EventName = lastAddedComment.Event.Name,
                UserName = lastAddedComment.User.UserName,
                EventId = lastAddedComment.EventId,
            };

            var replies = this.db.Replies.Where(x => x.EventId == lastAddedComment.EventId && x.ComentaryId == lastAddedComment.Id).Select(x => new ReplyViewModel
            {
                ComentaryId = x.ComentaryId,
                Content = x.Content,
                ReplyDislikesCount = x.Dislikes.Count(),
                ReplyId = x.Id,
                ReplyLikesCount = x.Likes.Count(),
                UserName = x.User.UserName,
            }).ToList();
            result.Replies = replies;

            return result;
        }
    }
}
