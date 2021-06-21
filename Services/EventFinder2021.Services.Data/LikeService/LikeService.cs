namespace EventFinder2021.Services.Data.LikeService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EventFinder2021.Data.Common.Repositories;
    using EventFinder2021.Data.Models;

    public class LikeService : ILikeService
    {
        private readonly IDeletableEntityRepository<Like> likeRepository;
        private readonly IDeletableEntityRepository<Comentary> comentaryRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public LikeService(IDeletableEntityRepository<Like> likeRepository, IDeletableEntityRepository<Comentary> comentaryRepository, IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.likeRepository = likeRepository;
            this.comentaryRepository = comentaryRepository;
            this.userRepository = userRepository;
        }

        public async Task AddComentaryLike(string userId, int comentaryId)
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
                this.comentaryRepository.Update(comentary);
                await this.comentaryRepository.SaveChangesAsync();
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
                await this.comentaryRepository.SaveChangesAsync();
            }
        }

        public async Task AddReplyLike(string userId, int replyId)
        {
            throw new NotImplementedException();
        }

        public int GetComentaryLikes(int comentaryId)
        {
            return this.likeRepository.All().Where(x => x.ComentaryId == comentaryId).ToList().Count();
        }

        public int GetReplyLikes(int replyId)
        {
            return this.likeRepository.All().Where(x => x.ReplyId == replyId).ToList().Count();
        }
    }
}
