namespace EventFinder2021.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using EventFinder2021.Data.Models;
    using EventFinder2021.Services.Data.ComentaryService;
    using EventFinder2021.Services.Data.DislikeService;
    using EventFinder2021.Services.Data.LikeService;
    using EventFinder2021.Web.Hubs;
    using EventFinder2021.Web.ViewModels.ComentaryModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class ComentaryController : Controller
    {
        private readonly IComentaryService comentaryService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILikeService likeService;
        private readonly IDislikeService dislikeService;

        public ComentaryController(
            IComentaryService comentaryService, UserManager<ApplicationUser> userManager, ILikeService likeService, IDislikeService dislikeService)
        {
            this.comentaryService = comentaryService;
            this.userManager = userManager;
            this.likeService = likeService;
            this.dislikeService = dislikeService;
        }

        
        public IActionResult AllComentaries([FromRoute] string id)
        {
            var eventId = int.Parse(id);
            var comentaries = this.comentaryService.GetAllEventComentaries<ComentaryViewModel>(eventId);

            return this.Json(comentaries);
        }

        [Authorize]
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> WriteComentary([FromBody] RePostComentaryModel model)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.UserId = userId;
            await this.comentaryService.WriteCommentaryAsync(model);
            return this.Redirect($"/Event/EventView/{model.EventId}");
        }

        [HttpPost]
        [Authorize]

        [IgnoreAntiforgeryToken]
        public IActionResult LikeComentary([FromBody] LikeComentaryInputModel model)
        {
            int comentaryId = int.Parse(model.ComentaryId);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            this.likeService.AddComentaryLike(userId, comentaryId);
            var comentaryLikesCount = this.likeService.GetComentaryLikesAndDislikes(comentaryId);
            return this.Json(comentaryLikesCount);
        }

        [HttpPost]
        [Authorize]
        [IgnoreAntiforgeryToken]
        public IActionResult DislikeComentary([FromBody] LikeComentaryInputModel model)
        {
            int comentaryId = int.Parse(model.ComentaryId);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            this.dislikeService.AddComentaryDislike(userId, comentaryId);
            var comentaryLikesCount = this.likeService.GetComentaryLikesAndDislikes(comentaryId);
            return this.Json(comentaryLikesCount);
        }
    }
}
