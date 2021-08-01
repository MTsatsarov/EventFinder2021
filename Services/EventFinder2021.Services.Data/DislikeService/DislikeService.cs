namespace EventFinder2021.Services.Data.DislikeService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using EventFinder2021.Data;
    using EventFinder2021.Data.Models;
    using EventFinder2021.Web.ViewModels.LikeDislikeViewModel;

    public class DislikeService : IDislikeService
    {
        private readonly ApplicationDbContext db;

        public DislikeService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void AddComentaryDislike(string userId, int comentaryId)
        {
            var comentary = this.db.Comentaries.Where(x => x.Id == comentaryId).FirstOrDefault();

            if (comentary == null)
            {
                throw new ArgumentNullException("The comentary you wish to dislike doesn't exists.");
            }

            var dislike = comentary.Dislikes.Where(x => x.Users.All(x => x.Id == userId)).FirstOrDefault();

            if (dislike != null)
            {
                return;
            }
            else
            {
                dislike = new Dislike()
                {
                    ComentaryId = comentaryId,
                    Comentary = comentary,
                };

                var currUser = this.db.Users.Where(x => x.Id == userId).First();

                var currLike = this.db.Likes.Where(x => x.ComentaryId == comentaryId && x.Users.Contains(currUser)).FirstOrDefault();
                if (currLike != null)
                {
                    currLike.IsDeleted = true;
                }

                dislike.Users.Add(currUser);
                comentary.Dislikes.Add(dislike);
                this.db.Dislikes.Add(dislike);
                this.db.SaveChanges();
            }
        }

        public void AddReplyDislike(string userId, int replyId)
        {
            var reply = this.db.Replies.Where(x => x.Id == replyId).FirstOrDefault();

            if (reply == null)
            {
                throw new ArgumentNullException("The reply you wish to like doesn't exists.");
            }

            var dislike = reply.Dislikes.Where(x => x.Users.All(x => x.Id == userId)).FirstOrDefault();

            if (dislike != null)
            {
                reply.Dislikes.Remove(dislike);
                this.db.Replies.Update(reply);
                this.db.SaveChanges();
            }
            else
            {
                dislike = new Dislike()
                {
                    ReplyId = replyId,
                    Reply = reply,
                };

                var currUser = this.db.Users.Where(x => x.Id == userId).First();
                var currLike = this.db.Likes.Where(x => x.ReplyId == replyId && x.Users.Contains(currUser)).FirstOrDefault();
                if (currLike != null)
                {
                    return;
                }

                dislike.Users.Add(currUser);
                reply.Dislikes.Add(dislike);
                this.db.Dislikes.Add(dislike);
                this.db.SaveChanges();
            }
        }

        public int GetReplyDislikes(int replyId)
        {
            return this.db.Dislikes.Where(x => x.ReplyId == replyId).ToList().Count();
        }
    }
}
