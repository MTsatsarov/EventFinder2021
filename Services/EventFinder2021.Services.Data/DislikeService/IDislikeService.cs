namespace EventFinder2021.Services.Data.DislikeService
{
    using EventFinder2021.Web.ViewModels.LikeDislikeViewModel;

   public interface IDislikeService
    {
        LikeDislikeViewModel GetComentaryDislikes(int comentaryId);

        int GetReplyDislikes(int replyId);

        void AddComentaryDislike(string userId, int comentaryId);

        void AddReplyDislike(string userId, int replyId);
    }
}
