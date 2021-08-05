namespace EventFinder2021.Services.Data.ReplyService
{
    using System.Threading.Tasks;

    using EventFinder2021.Web.ViewModels.ComentaryModels;

    public interface IReplyService
    {
        Task WriteReply(PostReplyModel model);
    }
}
