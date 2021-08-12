namespace EventFinder2021.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using EventFinder2021.Data.Models;
    using EventFinder2021.Services.Data.DislikeService;
    using EventFinder2021.Services.Data.LikeService;
    using EventFinder2021.Services.Data.ReplyService;
    using EventFinder2021.Web.ViewModels.ComentaryModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ReplyController : Controller
    {
        private readonly IReplyService replyService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILikeService likeService;
        private readonly IDislikeService dislikeService;

        public ReplyController(IReplyService replyService, UserManager<ApplicationUser> userManager, ILikeService likeService, IDislikeService dislikeService)
        {
            this.replyService = replyService;
            this.userManager = userManager;
            this.likeService = likeService;
            this.dislikeService = dislikeService;
        }

        [Authorize]
        public IActionResult WriteReply(int id)
        {
            var model = new PostReplyModel()
            {
                ComentaryId = id,
                UserId = this.userManager.GetUserId(this.User),
            };
            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> WriteReply([FromBody] PostReplyModel reply)
        {
            var user = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            reply.UserId = user;
            await this.replyService.WriteReply(reply);
            return this.Redirect("/");
        }

        [HttpPost]
        [Authorize]
        [IgnoreAntiforgeryToken]
        public IActionResult LikeReply([FromBody] LikeReplyInputModel model)
        {
            int replyId = int.Parse(model.ReplyId);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            this.likeService.AddReplyLike(userId, replyId);
            var replyLikesAndDislikes = this.likeService.GetReplyLikesAndDislikes(replyId);
            return this.Json(replyLikesAndDislikes);
        }

        [HttpPost]
        [Authorize]
        [IgnoreAntiforgeryToken]
        public IActionResult DislikeReply([FromBody] LikeReplyInputModel model)
        {
            var replyId = int.Parse(model.ReplyId);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            this.dislikeService.AddReplyDislike(userId, replyId);
            var replyLikesAndDislikes = this.likeService.GetReplyLikesAndDislikes(replyId);
            return this.Json(replyLikesAndDislikes);
        }
    }
}
