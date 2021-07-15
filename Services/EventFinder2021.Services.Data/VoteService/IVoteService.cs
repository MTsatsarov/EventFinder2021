namespace EventFinder2021.Services.Data.VoteService
{
    using System.Threading.Tasks;

    public interface IVoteService
    {
        Task EventVote(int eventId, string userId, byte grade);

        double GetAverageVoteValue(int eventId);
    }
}
