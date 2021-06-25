namespace EventFinder2021.Web.ViewModels.ComentaryModels
{
    public class ReplyViewModel
    {
        public int ComentaryId { get; set; }

        public int ReplyId { get; set; }

        public string Content { get; set; }

        public string UserName { get; set; }

        public int ReplyLikesCount { get; set; }

        public int ReplyDislikesCount { get; set; }
    }
}
