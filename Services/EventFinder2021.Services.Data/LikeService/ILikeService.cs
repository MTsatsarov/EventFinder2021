namespace EventFinder2021.Services.Data.LikeService
{
    using EventFinder2021.Web.ViewModels.LikeDislikeViewModel;

    public interface ILikeService
    {
        LikeDislikeViewModel GetComentaryLikesAndDislikes(int comentaryId);

        int GetReplyLikes(int replyId);

        void AddComentaryLike(string userId, int comentaryId);

        void AddReplyLike(string userId, int replyId);
    }
}
