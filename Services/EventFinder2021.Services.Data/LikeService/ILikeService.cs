namespace EventFinder2021.Services.Data.LikeService
{
    using System.Threading.Tasks;

    public interface ILikeService
    {
        int GetComentaryLikes(int comentaryId);

        int GetReplyLikes(int replyId);

        void AddComentaryLike(string userId, int comentaryId);

        void AddReplyLike(string userId, int replyId);
    }
}
