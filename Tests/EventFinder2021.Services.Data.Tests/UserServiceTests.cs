namespace EventFinder2021.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using EventFinder2021.Data;
    using EventFinder2021.Data.Models;
    using EventFinder2021.Services.Data.UserService;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class UserServiceTests
    {
        private ApplicationUser user;
        private Event inputModel;

        public UserServiceTests()
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

        public async Task AssertReturnUSersCountReturnsProperCount()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("NoMoreThanTenUsers");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var service = new UserService(dbContext);
            await dbContext.Users.AddAsync(this.user);
            await dbContext.Users.AddAsync(new ApplicationUser() { UserName = "Gosho" });
            await dbContext.Users.AddAsync(new ApplicationUser() { UserName = "Pesho" });
            await dbContext.SaveChangesAsync();

            Assert.Equal(3, service.GetTotalCountOfUsers());
        }

        [Fact]
        public async Task AssertReturnTopTenUsersReturnsMaximumTenUsers()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("totalUsersCount");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var service = new UserService(dbContext);

            for (int i = 0; i < 11; i++)
            {
                await dbContext.Users.AddAsync(new ApplicationUser() { UserName = $"Pesho+{i}" });
            }

            await dbContext.SaveChangesAsync();
            var totalUsers = service.TopTenUsers();
            Assert.Equal(10, totalUsers.Count());
        }

        [Fact]
        public async Task AssertTopTenUsersReturnTotalCountIfLessThanTen()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("LessThan10Users");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var service = new UserService(dbContext);

            for (int i = 0; i < 5; i++)
            {
                await dbContext.Users.AddAsync(new ApplicationUser() { UserName = $"Pesho+{i}" });
            }

            await dbContext.SaveChangesAsync();
            var totalUsers = service.TopTenUsers();
            Assert.Equal(5, totalUsers.Count());
        }

        [Fact]
        public async Task AssertTopTenUsersReturnsUsersOrderedByEventCount()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("LessThan10Users");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            var service = new UserService(dbContext);
            await dbContext.Events.AddAsync(this.inputModel);
            await dbContext.Events.AddAsync(this.inputModel);
            await dbContext.Users.AddAsync(this.user);
            await dbContext.Users.AddAsync(new ApplicationUser() { UserName = "Gosho" });
            await dbContext.Users.AddAsync(new ApplicationUser() { UserName = "Pesho" });
            await dbContext.SaveChangesAsync();
            var totalUsers = service.TopTenUsers().ToList();
            Assert.Equal(this.user.UserName, totalUsers[0].UserName);
        }
    }
}
