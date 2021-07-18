namespace EventFinder2021.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using EventFinder2021.Data.Models;
    using EventFinder2021.Services.Data.VoteService;
    using EventFinder2021.Web.ViewModels.VotesModel;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class VotesController : BaseController
    {
        private readonly IVoteService voteService;
        private readonly UserManager<ApplicationUser> userManager;

        public VotesController(IVoteService voteService, UserManager<ApplicationUser> userManager)
        {
            this.voteService = voteService;
            this.userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<VoteViewModel>> VoteForEvent(VoteInputModel model)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await this.voteService.EventVote(model.EventId, userId, model.Grade);
            var averageGrade = this.voteService.GetAverageVoteValue(model.EventId);
            return this.Json(new VoteViewModel { AverageVoteValue = averageGrade });
        }
    }
}
