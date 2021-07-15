namespace EventFinder2021.Services.Data.VoteService
{
    using System.Linq;
    using System.Threading.Tasks;

    using EventFinder2021.Data;
    using EventFinder2021.Data.Models;

    public class VoteService : IVoteService
    {
        private readonly ApplicationDbContext db;

        public VoteService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task EventVote(int eventId, string userId, byte grade)
        {
            var vote = this.db.Votes.Where(x => x.EventId == eventId && x.UserId == userId).FirstOrDefault();

            if (vote == null)
            {
                vote = new Vote()
                {
                    EventId = eventId,
                    UserId = userId,
                };
                await this.db.Votes.AddAsync(vote);
            }

            vote.Grade = grade;

            await this.db.SaveChangesAsync();
        }

        public double GetAverageVoteValue(int eventId)
        {
            var averageVoteGrade = this.db.Votes.Where(x => x.EventId == eventId).ToList();

            if (averageVoteGrade.Count() == 0)
            {
                return 0.0;
            }

            return averageVoteGrade.Average(x => x.Grade);
        }
    }
}
