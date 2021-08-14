namespace EventFinder2021.Services.Data.Tests
{
    using System;
    using System.Threading.Tasks;

    using EventFinder2021.Data;
    using EventFinder2021.Data.Models;
    using EventFinder2021.Services.Data.ComentaryService;
    using EventFinder2021.Services.Data.ReplyService;
    using EventFinder2021.Web.ViewModels.ComentaryModels;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class ReplyServiceTests
    {
        private ApplicationUser user;
        private Event inputModel;

        public ReplyServiceTests()
        {
            this.user = new ApplicationUser()
            {
                UserName = "User",
            };
            this.inputModel = new Event()
            {
                Category = (EventFinder2021.Data.Models.Enums.Category)1,
                City = (EventFinder2021.Data.Models.Enums.City)1,
                Description = "aaaaaaaaaaaaaaaa",
                User = this.user,
                Name = "Name",
                Date = DateTime.Now,
            };
        }

        [Fact]
        public void IfInvalidUserThrowException()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("InvalidUserWriteReply");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var service = new ReplyService(dbContext);

            var model = new PostReplyModel()
            {
                ComentaryId = 123,
                EventId = 123213,
                Content = "Some content",
                UserId = "some id",
            };
            Assert.ThrowsAsync<ArgumentException>(() => service.WriteReply(model)).GetAwaiter().GetResult().Message.Contains("User not found");
        }

        [Fact]
        public async Task IfEventInvalidThrowException()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("InvalidEventWriteReply");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            await dbContext.Users.AddAsync(this.user);
            await dbContext.SaveChangesAsync();
            var service = new ReplyService(dbContext);

            var model = new PostReplyModel()
            {
                ComentaryId = 123,
                EventId = 123213,
                Content = "Some content",
                UserId = this.user.Id,
            };
            Assert.ThrowsAsync<ArgumentException>(() => service.WriteReply(model)).GetAwaiter().GetResult().Message.Contains("This event doesn't have a commentary with this id");
        }

        [Fact]
        public async Task IfEventDontHaveCommentWithThisIdThrowException()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("InvalidEventWriteReply");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            await dbContext.Users.AddAsync(this.user);
            await dbContext.Events.AddAsync(this.inputModel);
            await dbContext.SaveChangesAsync();
            var service = new ReplyService(dbContext);
            var commentService = new ComentaryService(dbContext);
            var commentModel = new RePostComentaryModel()
            {
                Content = "Some Event",
                EventId = 1,
                UserId = this.user.Id,
            };
            await commentService.WriteCommentaryAsync(commentModel);
            var model = new PostReplyModel()
            {
                ComentaryId = 123213,
                EventId = 1,
                Content = "Some content",
                UserId = this.user.Id,
            };
            Assert.ThrowsAsync<ArgumentException>(() => service.WriteReply(model)).GetAwaiter().GetResult().Message.Contains("This event doesn't have a commentary with this id");
        }
    }
}
