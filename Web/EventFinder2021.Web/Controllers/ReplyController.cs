namespace EventFinder2021.Web.Controllers
{
    using EventFinder2021.Data.Models;
    using EventFinder2021.Services.Data.ReplyService;
    using EventFinder2021.Services.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class ReplyController : Controller
    {
        private readonly IReplyService replyService;
        private readonly UserManager<ApplicationUser> userManager;

        public ReplyController(IReplyService replyService, UserManager<ApplicationUser> userManager)
        {
            this.replyService = replyService;
            this.userManager = userManager;
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
    }
}
