namespace EventFinder2021.Web.Controllers
{
    using System.Threading.Tasks;

    using EventFinder2021.Data.Models;
    using EventFinder2021.Services.Data.DislikeService;
    using EventFinder2021.Services.Data.LikeService;
    using EventFinder2021.Services.Data.ReplyService;
    using EventFinder2021.Services.Models;
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
        public async Task<IActionResult> WriteReply(PostReplyModel reply)
        {
            await this.replyService.WriteReply(reply);
            return this.Redirect("/");
        }

        [HttpPost]
        [Authorize]
        public IActionResult LikeReply([FromBody] LikeReplyInputModel model)
        {
            int replyId = int.Parse(model.ReplyId);
            this.likeService.AddReplyLike(model.UserId, replyId);
            var comentaryLikesCount = this.likeService.GetReplyLikes(replyId);
            return this.Json(new { count = $"{comentaryLikesCount}" });
        }

        [HttpPost]
        [Authorize]
        public IActionResult DislikeReply([FromBody] LikeReplyInputModel model)
        {
            var reply = model.ReplyId.Remove(model.ReplyId.Length - 1);
            int replyId = int.Parse(reply);
            this.dislikeService.AddReplyDislike(model.UserId, replyId);
            var replyDislikes = this.dislikeService.GetReplyDislikes(replyId);
            return this.Json(new { count = $"{replyDislikes}" });
        }
    }
}
