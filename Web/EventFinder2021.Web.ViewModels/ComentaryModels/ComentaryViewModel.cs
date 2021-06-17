namespace EventFinder2021.Web.ViewModels.ComentaryModels
{
    using System.Collections.Generic;

    public class ComentaryViewModel
    {
        public ComentaryViewModel()
        {
            this.Replies = new List<ReplyViewModel>();
        }
        public int ComentaryId { get; set; }

        public string Content { get; set; }

        public string UserName { get; set; }

        public string EventName { get; set; }

        public ICollection<ReplyViewModel> Replies { get; set; }
    }
}
