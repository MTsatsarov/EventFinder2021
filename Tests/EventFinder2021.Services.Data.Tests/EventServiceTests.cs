﻿namespace EventFinder2021.Services.Data.Tests
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using EventFinder2021.Data;
    using EventFinder2021.Data.Models;
    using EventFinder2021.Data.Models.Enums;
    using EventFinder2021.Services.Data.EventService;
    using EventFinder2021.Services.Data.VoteService;
    using EventFinder2021.Web.ViewModels.EventViewModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class EventServiceTests
    {
        private readonly CreateEventInputModel inputModel;
        private readonly ApplicationUser user;

        public EventServiceTests()
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
        public async Task CreateEventSuccsefullyCreatesEvent()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("CreateEventTest");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            var voteserivce = new VoteService(dbContext);
            var service = new EventService(dbContext, voteserivce);

            await service.CreateEventAsync(this.inputModel, "ss");

            var count = service.GetCount();
            Assert.Equal(1, count);
        }

        [Fact]
        public async Task WhenDeleteEventNoLongerVisible()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("DeleteEventTest");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            var voteserivce = new VoteService(dbContext);
            var service = new EventService(dbContext, voteserivce);

            await service.CreateEventAsync(this.inputModel, "ss");
            await service.DeleteEvent(1);
            var count = service.GetCount();
            Assert.Equal(0, count);
        }

        [Fact]

        public async Task WhenCallUserEventsReturnUserEvents()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
    .UseInMemoryDatabase("UserEventTest");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            var voteserivce = new VoteService(dbContext);
            var service = new EventService(dbContext, voteserivce);

            dbContext.Users.Add(this.user);
            await dbContext.SaveChangesAsync();
            await service.CreateEventAsync(this.inputModel, "ss");
            var userEvents = service.GetEventsByUser(this.user.Id).ToList();
            Assert.Equal(this.user.Events.Count(), userEvents.Count());
        }

        [Fact]
        public async Task WhenCallEventByIdReturnsOnlyThisEvent()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
.UseInMemoryDatabase("ReturnEventsByIdTest");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            var voteserivce = new VoteService(dbContext);
            var service = new EventService(dbContext, voteserivce);

            await service.CreateEventAsync(this.inputModel, "ss");
            await service.CreateEventAsync(this.inputModel, "ss");
            await service.CreateEventAsync(this.inputModel, "ss");

            var returnedEvent = service.GetEventById(3);

            Assert.Equal(3, returnedEvent.Id);
        }

        [Fact]

        public async Task WhenCallGetAllEventsReturnsAllEvents()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
    .UseInMemoryDatabase("ReturnAllEventsTest");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            var voteserivce = new VoteService(dbContext);
            var service = new EventService(dbContext, voteserivce);

            await service.CreateEventAsync(this.inputModel, "ss");
            await service.CreateEventAsync(this.inputModel, "ss");
            await service.CreateEventAsync(this.inputModel, "ss");

            var allEvents = service.GetAllEvents(1, 12);

            Assert.Equal(3, allEvents.Count());
        }


        [Fact]

        public async Task WhenSearchEventByCityAndCategoryReturnsEventByCityAndCategory()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
  .UseInMemoryDatabase("ReturnEventsByCityAndCategory");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            var voteserivce = new VoteService(dbContext);
            var service = new EventService(dbContext, voteserivce);

            await service.CreateEventAsync(this.inputModel, "ss");
            this.inputModel.City = Enum.Parse<City>("Burgas");
            this.inputModel.Category = Enum.Parse<Category>("Art");
            this.inputModel.Name = "NewName";
            await service.CreateEventAsync(this.inputModel, "ss");
            await service.CreateEventAsync(this.inputModel, "ss");

            var searchedEvents = new EventSearchModel()
            {
                City = Enum.Parse<City>("Burgas"),
                Category = Enum.Parse<Category>("Art"),
            };
            var events = service.GetSearchedEvents(searchedEvents).ToList();

            Assert.Equal(2, events.Count());

            Assert.Equal(2, events[0].Id);
            Assert.Equal(3, events[1].Id);
        }

        [Fact]

        public async Task WhenSearchByNameReturnesEventsByName()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
 .UseInMemoryDatabase("ReturnEventsByCityAndCategory");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            var voteserivce = new VoteService(dbContext);
            var service = new EventService(dbContext, voteserivce);

            await service.CreateEventAsync(this.inputModel, "ss");
            this.inputModel.City = Enum.Parse<City>("Burgas");
            this.inputModel.Category = Enum.Parse<Category>("Art");
            this.inputModel.Name = "NewName";
            await service.CreateEventAsync(this.inputModel, "ss");
            await service.CreateEventAsync(this.inputModel, "ss");

            var searchedEvents = new EventSearchModel()
            {
                City = Enum.Parse<City>("Burgas"),
                Category = Enum.Parse<Category>("Art"),
                Name = "NewName",
            };
            var events = service.GetSearchedEvents(searchedEvents).ToList();

            Assert.Equal(2, events.Count());
            foreach (var currEvent in events)
            {
                Assert.Equal("NewName", currEvent.Name);
            }
        }

        [Fact]

        public async Task WhenAddsGoingUsersReturnsProperCount()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
 .UseInMemoryDatabase("ReturnsCorrectCountUsersGoingToEvent");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            var voteserivce = new VoteService(dbContext);
            var service = new EventService(dbContext, voteserivce);

            await service.CreateEventAsync(this.inputModel, "ss");

            var userTwo = new ApplicationUser()
            {
                UserName = "UserTwo",
            };

            dbContext.Users.Add(this.user);
            dbContext.Users.Add(userTwo);
            await dbContext.SaveChangesAsync();
            var oneUserCount = service.AddGoingUser(this.user.Id, 1);
            var twoUsersCount = service.AddGoingUser(userTwo.Id, 1);

            Assert.Equal(1, oneUserCount);
            Assert.Equal(2, twoUsersCount);
        }

        [Fact]

        public async Task WhenAddGoingUserThatNotExistsThrowsException()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
.UseInMemoryDatabase("AddNotExistedUserToGoingUsers");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            var voteserivce = new VoteService(dbContext);
            var service = new EventService(dbContext, voteserivce);

            await service.CreateEventAsync(this.inputModel, "ss");

            Assert.Throws<ArgumentNullException>(() => service.AddGoingUser(this.user.Id, 1)).Message.Contains("User not found");
        }


        [Fact]
        public async Task WhennAddGoingUserToNotExistingEventThrowsException()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
.UseInMemoryDatabase("AddNotExistedUserToGoingUsers");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            var voteserivce = new VoteService(dbContext);
            var service = new EventService(dbContext, voteserivce);
            dbContext.Users.Add(this.user);
            await dbContext.SaveChangesAsync();
            Assert.Throws<InvalidOperationException>(() => service.AddGoingUser(this.user.Id, 242424)).Message.Contains("Event not found");
        }
    }
}
