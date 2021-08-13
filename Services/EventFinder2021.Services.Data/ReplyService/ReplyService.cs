namespace EventFinder2021.Services.Data.ReplyService
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using EventFinder2021.Data;
    using EventFinder2021.Data.Models;
    using EventFinder2021.Web.ViewModels.ComentaryModels;

    public class ReplyService : IReplyService
    {
        private readonly ApplicationDbContext db;

        public ReplyService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task WriteReply(PostReplyModel model)
        {
            var currUser = this.db.Users.Where(x => x.Id == model.UserId).FirstOrDefault();
            if (currUser == null)
            {
                throw new ArgumentException("User not found.");
            }

            var currEvent = this.db.Events.Where(x => x.Id == model.EventId).FirstOrDefault();
            if (currEvent == null)
            {
                throw new ArgumentException("Event not found.");
            }

            if (!currEvent.Comentaries.Any(x => x.Id == model.ComentaryId))
            {
                throw new ArgumentException("This event doesn't have a commentary with this id");
            }

            var currReply = new Reply()
            {
                ComentaryId = model.ComentaryId,
                Content = model.Content,
                UserId = model.UserId,
                Comentary = this.db.Comentaries.Where(x => x.Id == model.ComentaryId).FirstOrDefault(),
                EventId = model.EventId,
            };
            await this.db.Replies.AddAsync(currReply);
            await this.db.SaveChangesAsync();
        }
    }
}
