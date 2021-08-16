namespace EventFinder2021.Services.Data.LikeService
{
    using System;
    using System.Linq;

    using EventFinder2021.Data;
    using EventFinder2021.Data.Common.Repositories;
    using EventFinder2021.Data.Models;
    using EventFinder2021.Web.ViewModels.LikeDislikeViewModel;

    public class LikeService : ILikeService
    {
        private readonly ApplicationDbContext db;

        public LikeService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void AddComentaryLike(string userId, int comentaryId)
        {
            var comentary = this.db.Comentaries.Where(x => x.Id == comentaryId).FirstOrDefault();

            if (comentary == null)
            {
                throw new ArgumentException("The comentary you wish to like doesn't exists.");
            }

            var like = comentary.Likes.Where(x => x.Users.All(x => x.Id == userId)).FirstOrDefault();

            if (like != null)
            {
                return;
            }
            else
            {
                like = new Like()
                {
                    ComentaryId = comentaryId,
                    Comentary = comentary,
                };

                var currUser = this.db.Users.Where(x => x.Id == userId).First();
                var dislike = this.db.Dislikes.Where(x => x.ComentaryId == comentaryId && x.Users.Contains(currUser)).FirstOrDefault();
                if (dislike != null)
                {
                    dislike.IsDeleted = true;
                }

                like.Users.Add(currUser);
                comentary.Likes.Add(like);
                this.db.Likes.Add(like);
                this.db.SaveChanges();
            }
        }

        public void AddReplyLike(string userId, int replyId)
        {
            var reply = this.db.Replies.Where(x => x.Id == replyId).FirstOrDefault();

            if (reply == null)
            {
                throw new ArgumentException("The reply you wish to like doesn't exists.");
            }

            var like = reply.Likes.Where(x => x.Users.All(x => x.Id == userId)).FirstOrDefault();

            if (like != null)
            {
                return;
            }
            else
            {
                like = new Like()
                {
                    ReplyId = replyId,
                    Reply = reply,
                };

                var currUser = this.db.Users.Where(x => x.Id == userId).First();

                var currDislike = this.db.Dislikes.Where(x => x.ReplyId == replyId && x.Users.Contains(currUser)).FirstOrDefault();
                if (currDislike != null)
                {
                    currDislike.IsDeleted = true;
                }

                like.Users.Add(currUser);
                reply.Likes.Add(like);
                this.db.Likes.Add(like);
                this.db.SaveChanges();
            }
        }

        public LikeDislikeViewModel GetComentaryLikesAndDislikes(int comentaryId)
        {
            var currComment = this.db.Comentaries.FirstOrDefault(x => x.Id == comentaryId);
            if (currComment == null)
            {
                throw new ArgumentException("Comment not found");
            }

            int likesCount = currComment.Likes.Count();
            var dislikeCount = currComment.Dislikes.Count();
            var likeDislikeModel = new LikeDislikeViewModel()
            {
                ComentaryLikeCount = likesCount,
                ComentaryDislikeCount = dislikeCount,
            };

            return likeDislikeModel;
        }

        public LikeDislikeViewModel GetReplyLikesAndDislikes(int replyId)
        {
            var currReply = this.db.Replies.FirstOrDefault(x => x.Id == replyId);

            if (currReply == null)
            {
                throw new ArgumentException("Invalid reply");
            }
            int likesCount = currReply.Likes.Count();
            var dislikeCount = currReply.Dislikes.Count();
            var likeDislikeModel = new LikeDislikeViewModel()
            {
                ComentaryLikeCount = likesCount,
                ComentaryDislikeCount = dislikeCount,
            };

            return likeDislikeModel;
        }
    }
}
