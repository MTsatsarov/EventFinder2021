namespace EventFinder2021.Services.Data.UserService
{
    using System.Collections.Generic;
    using System.Linq;

    using EventFinder2021.Data;
    using EventFinder2021.Web.ViewModels.UserViewModels;

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext db;

        public UserService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public int GetTotalCountOfUsers()
        {
            return this.db.Users.Count();
        }

        public IEnumerable<TopUsersByEventsViewModel> TopTenUsers()
        {
            var topTenUsers = this.db.Users.OrderByDescending(x => x.Events.Count()).Take(10).ToList();

            var usersList = new List<TopUsersByEventsViewModel>();

            foreach (var user in topTenUsers)
            {
                var currUser = new TopUsersByEventsViewModel()
                {
                    EventCount = user.Events.Count(),
                    UserName = user.UserName,
                };
                usersList.Add(currUser);
            }

            return usersList;
        }
    }
}
