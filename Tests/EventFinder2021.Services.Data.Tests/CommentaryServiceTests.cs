namespace EventFinder2021.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using EventFinder2021.Data;
    using EventFinder2021.Data.Models;
    using EventFinder2021.Services.Data.ComentaryService;
    using EventFinder2021.Services.Mapping;
    using EventFinder2021.Web.ViewModels;
    using EventFinder2021.Web.ViewModels.ComentaryModels;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class CommentaryServiceTests
    {
        private readonly Event inputModel;
        private readonly ApplicationUser user;

        public CommentaryServiceTests()
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
        public async Task SuccesfullyAddComment()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("successComment");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            var comentaryService = new ComentaryService(dbContext);
            await dbContext.Events.AddAsync(this.inputModel);
            var comment = new RePostComentaryModel()
            {
                Content = "asd",
                EventId = 1,
                UserId = this.user.Id,
            };

            await comentaryService.WriteCommentaryAsync(comment);
            await comentaryService.WriteCommentaryAsync(comment);

            Assert.Equal(2, await dbContext.Comentaries.CountAsync());
        }
        [Fact]
        public async Task IfInvalidUsersSentCommentsThrowException()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("successComment");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            var comentaryService = new ComentaryService(dbContext);
            await dbContext.Events.AddAsync(this.inputModel);
            var comment = new RePostComentaryModel()
            {
                Content = "asd",
                EventId = 1,
                UserId = "asdsa",
            };

            Assert.Throws<ArgumentException>(() => comentaryService.WriteCommentaryAsync(comment).GetAwaiter().GetResult()).Message.Contains($"No user with asdsa found.");
        }

        [Fact]
        public async Task IfInvalidEventsSentCommentsThrowException()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("successComment");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            var comentaryService = new ComentaryService(dbContext);
            await dbContext.Users.AddAsync(this.user);
            await dbContext.SaveChangesAsync();
            var comment = new RePostComentaryModel()
            {
                Content = "asd",
                EventId = 241231224,
                UserId = this.user.Id,
            };

            Assert.Throws<ArgumentException>(() => comentaryService.WriteCommentaryAsync(comment).GetAwaiter().GetResult()).Message.Contains($"No event with id {241231224} found.");
        }

        [Fact]
        public async Task CommentCountReturnsProperCommentaryCount()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("thissDb");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            var comentaryService = new ComentaryService(dbContext);
            await dbContext.Events.AddAsync(this.inputModel);
            await dbContext.Users.AddAsync(this.user);
            await dbContext.SaveChangesAsync();
            var comment = new RePostComentaryModel()
            {
                Content = "asd",
                EventId = 1,
                UserId = this.user.Id,
            };
            await comentaryService.WriteCommentaryAsync(comment);

            var count = comentaryService.GetComentaryCount(1);

            Assert.Equal(1, count);
        }

        [Fact]
        public void IfInvalidEventIdThrowException()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("thissDb");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            var comentaryService = new ComentaryService(dbContext);
            Assert.Throws<ArgumentException>(() => comentaryService.GetComentaryCount(1123123123)).Message.Contains("No event were found");
        }
    }
}
