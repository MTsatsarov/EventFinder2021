namespace EventFinder2021.Web.ViewModels.VotesModel
{
    using System.ComponentModel.DataAnnotations;

    public class VoteInputModel
    {
        public int EventId { get; set; }

        [Range(1, 5)]
        public byte Grade { get; set; }
    }
}
