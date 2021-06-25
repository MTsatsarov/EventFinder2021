namespace EventFinder2021.Web.Controllers
{
    using System.Threading.Tasks;

    using EventFinder2021.Data.Models;
    using EventFinder2021.Services.Data.ComentaryService;
    using EventFinder2021.Services.Data.DislikeService;
    using EventFinder2021.Services.Data.LikeService;
    using EventFinder2021.Services.Models;
    using EventFinder2021.Web.ViewModels.ComentaryModels;
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
        public IActionResult AllComentaries(string id)
        {
            var eventId = int.Parse(id);
            var comentaries = this.comentaryService.GetAllEventComentaries(eventId);

            return this.View(comentaries);
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
        public IActionResult LikeComentary([FromBody] LikeComentaryInputModel model)
        {
            int comentaryId = int.Parse(model.ComentaryId);
            this.likeService.AddComentaryLike(model.UserId, comentaryId);
            var comentaryLikesCount = this.likeService.GetComentaryLikes(comentaryId);
            return this.Json(new { count = $"{comentaryLikesCount}" });
        }

        [HttpPost]
        [Authorize]
        public IActionResult DislikeComentary([FromBody] LikeComentaryInputModel model)
        {
            var id = model.ComentaryId.Remove(model.ComentaryId.Length - 1);
            int comentaryId = int.Parse(id);
            this.dislikeService.AddComentaryDislike(model.UserId, comentaryId);
            var comentaryLikesCount = this.dislikeService.GetComentaryDislikes(comentaryId);
            return this.Json(new { count = $"{comentaryLikesCount}" });
        }
    }
}
