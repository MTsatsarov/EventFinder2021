namespace EventFinder2021.Services.Data.LikeService
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using EventFinder2021.Data;
    using EventFinder2021.Data.Common.Repositories;
    using EventFinder2021.Data.Models;

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
                comentary.Likes.Remove(like);
                this.db.Comentaries.Update(comentary);
                this.db.SaveChanges();
            }
            else
            {
                like = new Like()
                {
                    ComentaryId = comentaryId,
                    Comentary = comentary,
                };

                var currUser = this.userRepository.All().Where(x => x.Id == userId).First();
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
                like.Users.Add(currUser);
                reply.Likes.Add(like);
                this.db.Likes.Add(like);
                this.db.SaveChanges();
            }
        }

        public int GetComentaryLikes(int comentaryId)
        {
            return this.db.Likes.Where(x => x.ComentaryId == comentaryId).ToList().Count();
        }

        public int GetReplyLikes(int replyId)
        {
            return this.db.Likes.Where(x => x.ReplyId == replyId).ToList().Count();
        }
    }
}
