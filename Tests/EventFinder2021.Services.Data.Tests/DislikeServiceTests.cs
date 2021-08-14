namespace EventFinder2021.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using EventFinder2021.Data;
    using EventFinder2021.Data.Models;
    using EventFinder2021.Services.Data.ComentaryService;
    using EventFinder2021.Services.Data.DislikeService;
    using EventFinder2021.Services.Data.LikeService;
    using EventFinder2021.Services.Data.ReplyService;
    using EventFinder2021.Web.ViewModels.ComentaryModels;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class DislikeServiceTests
    {
        private ApplicationUser user;
        private Event inputModel;

        public DislikeServiceTests()
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
        public async Task WhenCommentaryIdInvalidThrowException()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("CreateEventTest");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            await dbContext.Users.AddAsync(this.user);
            await dbContext.Events.AddAsync(this.inputModel);
            await dbContext.SaveChangesAsync();
            var service = new DislikeService(dbContext);
            Assert.Throws<ArgumentException>(() => service.AddComentaryDislike(this.user.Id, 12321312)).Message.Contains("The comentary you wish to dislike doesn't exists.");
        }

        [Fact]
        public async Task WhenUserIdInvalidThrowsException()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("InvalidUser");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            await dbContext.Events.AddAsync(this.inputModel);
            await dbContext.Users.AddAsync(this.user);
            await dbContext.SaveChangesAsync();
            var service = new DislikeService(dbContext);
            var commentService = new ComentaryService(dbContext);
            var model = new RePostComentaryModel()
            {
                Content = "asdsadsadsadsa",
                EventId = 1,
                UserId = this.user.Id,
            };
            await commentService.WriteCommentaryAsync(model);

            Assert.Throws<ArgumentException>(() => service.AddComentaryDislike("Pesho", 12321312)).Message.Contains("User not found.");
        }

        [Fact]
        public async Task AssertIsSuccesfullyAdded()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("SuccesfullyDislikeComment");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            await dbContext.Events.AddAsync(this.inputModel);
            await dbContext.Users.AddAsync(this.user);
            await dbContext.SaveChangesAsync();
            var service = new DislikeService(dbContext);
            var commentService = new ComentaryService(dbContext);
            var model = new RePostComentaryModel()
            {
                Content = "asdsadsadsadsa",
                EventId = 1,
                UserId = this.user.Id,
            };
            await commentService.WriteCommentaryAsync(model);
            service.AddComentaryDislike(this.user.Id, 1);
            Assert.Equal(1, await dbContext.Dislikes.CountAsync());
        }

        [Fact]
        public async Task IfCommentAlreadyLikedByTheSameUserSetToDeleted()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("LikeIsDeleted");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            await dbContext.Events.AddAsync(this.inputModel);
            await dbContext.Users.AddAsync(this.user);
            await dbContext.SaveChangesAsync();
            var service = new DislikeService(dbContext);
            var commentService = new ComentaryService(dbContext);

            var likeService = new LikeService(dbContext);
            var model = new RePostComentaryModel()
            {
                Content = "asdsadsadsadsa",
                EventId = 1,
                UserId = this.user.Id,
            };

            await commentService.WriteCommentaryAsync(model);

            likeService.AddComentaryLike(this.user.Id, 1);
            var someLike = dbContext.Likes.FirstOrDefault();
            service.AddComentaryDislike(this.user.Id, 1);
            var currLike = dbContext.Likes.Find(1);

            Assert.Equal(someLike.Id, currLike.Id);
            Assert.True(currLike.IsDeleted == true);
            Assert.True(this.inputModel.Comentaries.First().Dislikes.Count() > 0);
        }

        [Fact]
        public async Task WhenReplyIdInvalidThrowException()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("InvalidReplyId");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            await dbContext.Users.AddAsync(this.user);
            await dbContext.Events.AddAsync(this.inputModel);
            await dbContext.SaveChangesAsync();
            var service = new DislikeService(dbContext);
            Assert.Throws<ArgumentException>(() => service.AddReplyDislike(this.user.Id, 12321312)).Message.Contains("The reply you wish to diislike doesn't exists.");
        }

        [Fact]
        public async Task WhenUserIdForReplyInvalidThrowsException()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("NewInvalidUser");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            await dbContext.Events.AddAsync(this.inputModel);
            await dbContext.Users.AddAsync(this.user);
            await dbContext.SaveChangesAsync();
            var service = new DislikeService(dbContext);
            var replyService = new ReplyService(dbContext);
            var model = new PostReplyModel()
            {
                Content = "asdsadsadsadsa",
                EventId = 1,
                UserId = this.user.Id,
            };
            await replyService.WriteReply(model);

            Assert.Throws<ArgumentException>(() => service.AddReplyDislike("Pesho", 12321312)).Message.Contains("User not found.");
        }

        [Fact]
        public async Task AssertReplyDislikeSuccesfullyAddedAndCountIsCorrect()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("Adasdasdaasd");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            await dbContext.Events.AddAsync(this.inputModel);
            await dbContext.Users.AddAsync(this.user);
            await dbContext.SaveChangesAsync();
            var service = new DislikeService(dbContext);
            var replyService = new ReplyService(dbContext);
            var model = new PostReplyModel()
            {
                Content = "asdsadsadsadsa",
                EventId = 1,
                UserId = this.user.Id,
            };
            await replyService.WriteReply(model);
            service.AddReplyDislike(this.user.Id, 1);
            Assert.Equal(1, await dbContext.Dislikes.CountAsync());

            var replyCount = service.GetReplyDislikes(1);

            Assert.Equal(1, replyCount);
        }

        [Fact]
        public async Task IfReplyAlreadyLikedByTheSameUserSetToDeleted()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("LikeReplyForSUreDeleted");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            await dbContext.Events.AddAsync(this.inputModel);
            await dbContext.Users.AddAsync(this.user);
            await dbContext.SaveChangesAsync();
            var service = new DislikeService(dbContext);
            var commentService = new ComentaryService(dbContext);
            var model = new RePostComentaryModel()
            {
                Content = "asdsadsadsadsa",
                EventId = 1,
                UserId = this.user.Id,
            };

            await commentService.WriteCommentaryAsync(model);
            var likeService = new LikeService(dbContext);
            var replyService = new ReplyService(dbContext);
            var replyModel = new PostReplyModel()
            {
                Content = "asdsadsadsadsa",
                EventId = 1,
                UserId = this.user.Id,
            };
            await replyService.WriteReply(replyModel);
            likeService.AddReplyLike(this.user.Id, 1);
            var someLike = dbContext.Likes.FirstOrDefault();
            service.AddReplyDislike(this.user.Id, 1);
            var currLike = dbContext.Likes.Find(1);

            Assert.Equal(someLike.Id, currLike.Id);
            Assert.True(currLike.IsDeleted == true);
            Assert.True(this.inputModel.Replies.First().Dislikes.Count() > 0);
        }
    }
}
