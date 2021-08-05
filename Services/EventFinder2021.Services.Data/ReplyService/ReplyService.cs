namespace EventFinder2021.Services.Data.ReplyService
{
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
