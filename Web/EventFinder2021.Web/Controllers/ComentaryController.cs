namespace EventFinder2021.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using EventFinder2021.Data.Models;
    using EventFinder2021.Services.Data.ComentaryService;
    using EventFinder2021.Services.Data.DislikeService;
    using EventFinder2021.Services.Data.LikeService;
    using EventFinder2021.Services.Models;
    using EventFinder2021.Web.ViewModels.ComentaryModels;
    using EventFinder2021.Web.ViewModels.LikeDislikeViewModel;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

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

        [Authorize]
        [IgnoreAntiforgeryToken]
        public IActionResult AllComentaries([FromBody]GetComentaryModel id)
        {
            var eventId = int.Parse(id.EventId);
            var comentaries = this.comentaryService.GetAllEventComentaries(eventId);

            return this.Json(comentaries);
        }

        [Authorize]
        public IActionResult WriteComentary(int id)
        {
            var model = new PostComentaryModel()
            {
                EventId = id,
                UserId = this.userManager.GetUserId(this.User),
            };
            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> WriteComentary(PostComentaryModel model)
        {
            await this.comentaryService.WriteComentary(model);
            return this.RedirectToAction("AllComentaries", new { id = $"{model.EventId}" });
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
