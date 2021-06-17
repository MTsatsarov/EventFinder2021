namespace EventFinder2021.Services.Data.ReplyService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using EventFinder2021.Data;
    using EventFinder2021.Data.Models;
    using EventFinder2021.Services.Models;

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
            };
            await this.db.Replies.AddAsync(currReply);
            await this.db.SaveChangesAsync();
        }
    }
}
