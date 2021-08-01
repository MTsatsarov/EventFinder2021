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
        private readonly IDeletableEntityRepository<Comentary> comentaryRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public LikeService(ApplicationDbContext db, IDeletableEntityRepository<Comentary> comentaryRepository, IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.db = db;
            this.comentaryRepository = comentaryRepository;
            this.userRepository = userRepository;
        }

        public void AddComentaryLike(string userId, int comentaryId)
        {
            var comentary = this.comentaryRepository.All().Where(x => x.Id == comentaryId).FirstOrDefault();

            if (comentary == null)
            {
                throw new ArgumentNullException("The comentary you wish to like doesn't exists.");
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

                var currUser = this.userRepository.All().Where(x => x.Id == userId).First();
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
                throw new ArgumentNullException("The reply you wish to like doesn't exists.");
            }

            var like = reply.Likes.Where(x => x.Users.All(x => x.Id == userId)).FirstOrDefault();

            if (like != null)
            {
                reply.Likes.Remove(like);
                this.db.Replies.Update(reply);
                this.db.SaveChanges();
            }
            else
            {
                like = new Like()
                {
                    ReplyId = replyId,
                    Reply = reply,
                };

                var currUser = this.userRepository.All().Where(x => x.Id == userId).First();

                var currLike = this.db.Dislikes.Where(x => x.ReplyId == replyId && x.Users.Contains(currUser)).FirstOrDefault();
                if (currLike != null)
                {
                    return;
                }

                like.Users.Add(currUser);
                reply.Likes.Add(like);
                this.db.Likes.Add(like);
                this.db.SaveChanges();
            }
        }

        public LikeDislikeViewModel GetComentaryLikesAndDislikes(int comentaryId)
        {
            int likesCount = this.db.Likes.Where(x => x.ComentaryId == comentaryId).ToList().Count;
            var dislikeCount = this.db.Dislikes.Where(x => x.ComentaryId == comentaryId).ToList().Count();
            var likeDislikeModel = new LikeDislikeViewModel()
            {
                ComentaryLikeCount = likesCount,
                ComentaryDislikeCount = dislikeCount,
            };

            return likeDislikeModel;
        }

        public LikeDislikeViewModel GetReplyLikesAndDislikes(int replyId)
        {
            int likesCount = this.db.Likes.Where(x => x.ReplyId == replyId).ToList().Count;
            var dislikeCount = this.db.Dislikes.Where(x => x.ReplyId == replyId).ToList().Count();
            var likeDislikeModel = new LikeDislikeViewModel()
            {
                ComentaryLikeCount = likesCount,
                ComentaryDislikeCount = dislikeCount,
            };

            return likeDislikeModel;
        }
    }
}
