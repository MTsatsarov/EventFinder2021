namespace EventFinder2021.Services.Data.Tests
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using EventFinder2021.Data;
    using EventFinder2021.Data.Models;
    using EventFinder2021.Services.Data.EventService;
    using EventFinder2021.Services.Data.VoteService;
    using EventFinder2021.Web.ViewModels.EventViewModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class EventServiceTests
    {


        [Fact]
        public async Task CreateEventSuccsefullyCreatesEvent()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("test");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            var voteserivce = new VoteService(dbContext);
            var service = new EventService(dbContext, voteserivce);

            var user = new ApplicationUser()
            {
                Id = "user",
            };

            var currEvent = new CreateEventInputModel()
            {
                Category = (EventFinder2021.Data.Models.Enums.Category)1,
                City = (EventFinder2021.Data.Models.Enums.City)1,
                Description = "aaaaaaaaaaaaaaaa",
                CreatedByuser = "user",
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
            currEvent.Image = fileMock.Object;

            await service.CreateEventAsync(currEvent, "ss");

            var eventById = service.GetEventById(1);
            var events = service.GetAllEvents(1, 12);
            Assert.Equal(currEvent.Name, eventById.Name);

            var count = service.GetCount();
            Assert.Equal(1, count);          
        }

        [Fact]
        public async Task WhenDeleteEventNoLongerVisible()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("test");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            var voteserivce = new VoteService(dbContext);
            var service = new EventService(dbContext, voteserivce);

            var currEvent = new CreateEventInputModel()
            {
                Category = (EventFinder2021.Data.Models.Enums.Category)1,
                City = (EventFinder2021.Data.Models.Enums.City)1,
                Description = "aaaaaaaaaaaaaaaa",
                CreatedByuser = "user",
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
            currEvent.Image = fileMock.Object;

            await service.CreateEventAsync(currEvent, "ss");
            await service.DeleteEvent(1);
            var count = service.GetCount();
            Assert.Equal(0, count);
        }
    }
}
