namespace EventFinder2021.Services.Data.DislikeService
{
   public interface IDislikeService
    {
        int GetComentaryDislikes(int comentaryId);

        int GetReplyDislikes(int replyId);

        void AddComentaryDislike(string userId, int comentaryId);

        void AddReplyDislike(string userId, int replyId);
    }
}
