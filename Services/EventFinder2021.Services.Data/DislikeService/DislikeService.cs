namespace EventFinder2021.Services.Data.DislikeService
{
    using System;
    using System.Linq;

    using EventFinder2021.Data;
    using EventFinder2021.Data.Models;

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
            var currentUser = this.db.Users.Where(x => x.Id == userId).FirstOrDefault();
            if (comentary == null)
            {
                throw new ArgumentException("The comentary you wish to dislike doesn't exists.");
            }

            if (currentUser == null)
            {
                throw new ArgumentException("User not found.");
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

                var currLike = this.db.Likes.Where(x => x.ComentaryId == comentaryId && x.Users.Contains(currentUser)).FirstOrDefault();
                if (currLike != null)
                {
                    currLike.IsDeleted = true;
                }

                dislike.Users.Add(currentUser);
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
                throw new ArgumentException("The reply you wish to diislike doesn't exists.");
            }

            var currentUser = this.db.Users.Where(x => x.Id == userId).FirstOrDefault();
            if (currentUser == null)
            {
                throw new ArgumentException("User not found");
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
                var currLike = this.db.Likes.Where(x => x.ReplyId == replyId && x.Users.Contains(currentUser)).FirstOrDefault();
                if (currLike != null)
                {
                    currLike.IsDeleted = true;
                }

                dislike.Users.Add(currentUser);
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
