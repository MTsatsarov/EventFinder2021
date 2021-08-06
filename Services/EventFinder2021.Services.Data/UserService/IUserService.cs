namespace EventFinder2021.Services.Data.UserService
{
    using System.Collections.Generic;

    using EventFinder2021.Web.ViewModels.UserViewModels;

    public interface IUserService
    {
        IEnumerable<TopUsersByEventsViewModel> TopTenUsers();

        int GetTotalCountOfUsers();
    }
}
