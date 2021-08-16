using EventFinder2021.Data;
using EventFinder2021.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace EventFinder2021.Services.Data.Tests
{
    public class LikeServiceTests
    {
        private ApplicationUser user;
        private Event inputModel;

        public LikeServiceTests()
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
        public void AssertThrowExceptionWhenComentaryIndvalid()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("CreateEventTestTestTest");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            var service = new LikeService.LikeService(dbContext);
            dbContext.Users.Add(this.user);
            dbContext.SaveChanges();
            Assert.Throws<ArgumentException>(() => service.AddComentaryLike(this.user.Id, 213213)).Message.Contains("The comentary you wish to like doesn't exists.");
        }

        [Fact]
        public async Task AssertIfAlreadyLikedDoNothing()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("doNothingWithLike1");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            var service = new LikeService.LikeService(dbContext);
            await dbContext.Users.AddAsync(this.user);
            dbContext.SaveChanges();
            await dbContext.Events.AddAsync(this.inputModel);
            await dbContext.SaveChangesAsync();
            var comment = new Comentary()
            {
                EventId = this.inputModel.Id,
                Content = "ASUHDuashdusad",
            };
            await dbContext.Comentaries.AddAsync(comment);
            await dbContext.SaveChangesAsync();
            service.AddComentaryLike(
                this.user.Id, comment.Id);
            service.AddComentaryLike(
               this.user.Id, comment.Id);

            Assert.Equal(1, await dbContext.Likes.CountAsync());
        }

        [Fact]
        public async Task AssertIfUserAlreadyDislikeDeleteDislike()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("deleteDislikee");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            var service = new LikeService.LikeService(dbContext);
            await dbContext.Users.AddAsync(this.user);
            dbContext.SaveChanges();
            await dbContext.Events.AddAsync(this.inputModel);
            await dbContext.SaveChangesAsync();
            var comment = new Comentary()
            {
                EventId = this.inputModel.Id,
                Content = "ASUHDuashdusad",
            };
            await dbContext.Comentaries.AddAsync(comment);
            await dbContext.SaveChangesAsync();
            var dislike = new Dislike()
            {
                ComentaryId = comment.Id,
            };
            dislike.Users.Add(this.user);
            await dbContext.Dislikes.AddAsync(dislike);
            await dbContext.SaveChangesAsync();
            Assert.Equal(1, await dbContext.Dislikes.CountAsync());
            service.AddComentaryLike(
                this.user.Id, comment.Id);

            Assert.Equal(0, await dbContext.Dislikes.CountAsync());
        }

        [Fact]
        public void AssertThrowExceptionIfReplyIdInvalid()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("InvalidReplyId");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            var service = new LikeService.LikeService(dbContext);
            dbContext.Users.Add(this.user);
            dbContext.SaveChanges();
            Assert.Throws<ArgumentException>(() => service.AddReplyLike(this.user.Id, 213213)).Message.Contains("The reply you wish to like doesn't exists.");
        }

        [Fact]
        public async Task DoNothingIfAlreadyLikeReply()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase("doNothingWithLike1");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            var service = new LikeService.LikeService(dbContext);
            await dbContext.Users.AddAsync(this.user);
            dbContext.SaveChanges();
            await dbContext.Events.AddAsync(this.inputModel);
            await dbContext.SaveChangesAsync();
            var replyId = new Reply()
            {
                ComentaryId = this.inputModel.Id,
                Content = "ASUHDuashdusad",
            };
            await dbContext.Replies.AddAsync(replyId);
            await dbContext.SaveChangesAsync();
            service.AddReplyLike(
                this.user.Id, replyId.Id);
            service.AddReplyLike(
               this.user.Id, replyId.Id);

            Assert.Equal(1, await dbContext.Likes.CountAsync());
        }

        [Fact]
        public async Task AssertIfUserAlreadyDislikeDeleteDIslikeReply()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                         .UseInMemoryDatabase("deleteDislikeeIfDisliked");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            var service = new LikeService.LikeService(dbContext);
            await dbContext.Users.AddAsync(this.user);
            dbContext.SaveChanges();
            await dbContext.Events.AddAsync(this.inputModel);
            await dbContext.SaveChangesAsync();
            var replyId = new Reply()
            {
                EventId = this.inputModel.Id,
                Content = "ASUHDuashdusad",
            };
            await dbContext.Replies.AddAsync(replyId);
            await dbContext.SaveChangesAsync();
            var dislike = new Dislike()
            {
                ReplyId = replyId.Id,
            };
            dislike.Users.Add(this.user);
            await dbContext.Dislikes.AddAsync(dislike);
            await dbContext.SaveChangesAsync();
            Assert.Equal(1, await dbContext.Dislikes.CountAsync());
            service.AddReplyLike(
                this.user.Id, replyId.Id);

            Assert.Equal(0, await dbContext.Dislikes.CountAsync());
        }

        [Fact]

        public async Task IfCommentIdInvalidWhenAskForLikesDislikesThrowException()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("CreateEventTestTestTest");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            var service = new LikeService.LikeService(dbContext);
            await dbContext.Users.AddAsync(this.user);
            await dbContext.SaveChangesAsync();
            Assert.Throws<ArgumentException>(() => service.GetComentaryLikesAndDislikes(13213)).Message.Contains("Comment not found");
        }

        [Fact]
        public async Task AssertGetCommentaryDislikeLikeReturnsProperCount()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("CreateEventTestTestTest");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            var service = new LikeService.LikeService(dbContext);
            dbContext.Users.Add(this.user);
            var userTwo = new ApplicationUser()
            {
                UserName = "hasdhasd",
            };
            await dbContext.Users.AddAsync(userTwo);
            dbContext.SaveChanges();
            await dbContext.Events.AddAsync(this.inputModel);
            await dbContext.SaveChangesAsync();
            var comment = new Comentary()
            {
                EventId = this.inputModel.Id,
                Content = "ASUHDuashdusad",
            };
            await dbContext.Comentaries.AddAsync(comment);
            await dbContext.SaveChangesAsync();

            service.AddComentaryLike(this.user.Id, comment.Id);
            service.AddComentaryLike(userTwo.Id, comment.Id);

            var currComment = service.GetComentaryLikesAndDislikes(comment.Id);
            var likes = currComment.ComentaryLikeCount;
            var dislikes = currComment.ComentaryDislikeCount;

            Assert.Equal(2, likes);
            Assert.Equal(0, dislikes);
        }

        [Fact]

        public async Task AssertGetReplyLikesDislikesThrowsExceptionIfInvalidId()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase("CreateEventTestTestTest");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            var service = new LikeService.LikeService(dbContext);
            await dbContext.Users.AddAsync(this.user);
            await dbContext.SaveChangesAsync();
            Assert.Throws<ArgumentException>(() => service.GetReplyLikesAndDislikes(13213)).Message.Contains("Invalid reply");
        }

        [Fact]
        public async Task AssertGetReplyLikesDislikesReturnsProperCount()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase("Testestests");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            var service = new LikeService.LikeService(dbContext);
            dbContext.Users.Add(this.user);
            var userTwo = new ApplicationUser()
            {
                UserName = "hasdhasd",
            };
            await dbContext.Users.AddAsync(userTwo);
            dbContext.SaveChanges();
            await dbContext.Events.AddAsync(this.inputModel);
            await dbContext.SaveChangesAsync();
            var comment = new Comentary()
            {
                EventId = this.inputModel.Id,
                Content = "ASUHDuashdusad",
            };
            await dbContext.Comentaries.AddAsync(comment);
            await dbContext.SaveChangesAsync();
            var reply = new Reply()
            {
                ComentaryId = comment.Id,

            };

            await dbContext.Replies.AddAsync(reply);
            await dbContext.SaveChangesAsync();
            service.AddReplyLike(this.user.Id, reply.Id);
            service.AddReplyLike(userTwo.Id, reply.Id);

            var currReply = service.GetReplyLikesAndDislikes(reply.Id);
            var likes = currReply.ComentaryLikeCount;
            var dislikes = currReply.ComentaryDislikeCount;

            Assert.Equal(2, likes);
            Assert.Equal(0, dislikes);
        }
    }
}
