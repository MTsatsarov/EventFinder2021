namespace EventFinder2021.Services.Data.ReplyService
{
    using System.Threading.Tasks;

    using EventFinder2021.Services.Models;

    public interface IReplyService
    {
        Task WriteReply(PostReplyModel model);
    }
}
