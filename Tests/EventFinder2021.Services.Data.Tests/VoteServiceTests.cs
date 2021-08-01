namespace EventFinder2021.Services.Data.Tests
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using EventFinder2021.Data;
    using EventFinder2021.Data.Models;
    using EventFinder2021.Web.ViewModels.EventViewModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class VoteServiceTests
    {
        private readonly CreateEventInputModel inputModel;
        private readonly ApplicationUser user;

        public VoteServiceTests()
        {
            this.user = new ApplicationUser()
            {
                UserName = "User",
            };
            this.inputModel = new CreateEventInputModel()
            {
                Category = (EventFinder2021.Data.Models.Enums.Category)1,
                City = (EventFinder2021.Data.Models.Enums.City)1,
                Description = "aaaaaaaaaaaaaaaa",
                CreatedByuser = "User",
                Name = "Name",
                Date = DateTime.Now,
            };

            // Arrange
            var fileMock = new Mock<IFormFile>();

            // Setup mock file using a memory stream
            var content = "Hello World from a Fake File";
            var fileName = "test.pdf";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);
            this.inputModel.Image = fileMock.Object;
        }

        [Fact]

        public async Task DefaultValueOfVotesForEventIsZero()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("zeroValueOfVotes");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            var voteserivce = new EventFinder2021.Services.Data.VoteService.VoteService(dbContext);
            var service = new EventFinder2021.Services.Data.EventService.EventService(dbContext, voteserivce);

            await service.CreateEventAsync(this.inputModel, "ss");
            var value = voteserivce.GetAverageVoteValue(1);
            Assert.Equal(0, value);
        }

        [Fact]
        public async Task CalculatesAverageVoteValueProperly()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("properAverageVoteValue");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            var voteserivce = new EventFinder2021.Services.Data.VoteService.VoteService(dbContext);
            var service = new EventFinder2021.Services.Data.EventService.EventService(dbContext, voteserivce);

            await service.CreateEventAsync(this.inputModel, "ss");
            await voteserivce.EventVote(1, this.user.Id, 5);
            var applicationUserTwo = new ApplicationUser()
            {
                UserName = "Pesho",
            };
            dbContext.Users.Add(applicationUserTwo);
            await dbContext.SaveChangesAsync();
            await voteserivce.EventVote(1, applicationUserTwo.Id, 4);
            var value = voteserivce.GetAverageVoteValue(1);
            Assert.Equal(4.5, value);
        }

        [Fact]
        public async Task WhenUserVotesMultiplyTimesTakesLastVote()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("multiplyTimesVote");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            var voteserivce = new EventFinder2021.Services.Data.VoteService.VoteService(dbContext);
            var service = new EventFinder2021.Services.Data.EventService.EventService(dbContext, voteserivce);

            await service.CreateEventAsync(this.inputModel, "ss");
            await voteserivce.EventVote(1, this.user.Id, 5);
            await voteserivce.EventVote(1, this.user.Id, 4);
            var value = voteserivce.GetAverageVoteValue(1);
            Assert.Equal(4, value);
        }

        [Fact]
        public async Task IfInvalidUsersVotesReturnException()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("invalidUser");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            var voteserivce = new EventFinder2021.Services.Data.VoteService.VoteService(dbContext);
            var service = new EventFinder2021.Services.Data.EventService.EventService(dbContext, voteserivce);

            await service.CreateEventAsync(this.inputModel, "ss");
            await Assert.ThrowsAsync<ArgumentException>(() => voteserivce.EventVote(1, this.user.Id, 1));
        }

        [Fact]
        public async Task IfInvalidEventIdThrowsException()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("invalidEvent");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            var voteserivce = new EventFinder2021.Services.Data.VoteService.VoteService(dbContext);
            var service = new EventFinder2021.Services.Data.EventService.EventService(dbContext, voteserivce);
            await dbContext.Users.AddAsync(this.user);
            await service.CreateEventAsync(this.inputModel, "ss");
            await Assert.ThrowsAsync<ArgumentException>(() => voteserivce.EventVote(232323, this.user.Id, 1));
        }

        [Fact]
        public async Task IfInvalidGradeThrowsException()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("invalidGrade");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            var voteserivce = new EventFinder2021.Services.Data.VoteService.VoteService(dbContext);
            var service = new EventFinder2021.Services.Data.EventService.EventService(dbContext, voteserivce);
            await dbContext.Users.AddAsync(this.user);
            await service.CreateEventAsync(this.inputModel, "ss");
            await Assert.ThrowsAsync<ArgumentException>(() => voteserivce.EventVote(1, this.user.Id, 42));
        }
    }
}
