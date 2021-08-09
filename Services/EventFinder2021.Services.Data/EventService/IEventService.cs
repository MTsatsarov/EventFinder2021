namespace EventFinder2021.Services.Data.EventService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EventFinder2021.Web.ViewModels.EventViewModels;

    public interface IEventService
    {
        Task CreateEventAsync(CreateEventInputModel model, string imagePath);

        IEnumerable<T> GetAllEvents<T>(int pageNumber, int itemsPerPage = 12);

        T GetEventById<T>(int id);

        int GetCount();

        IEnumerable<T> GetEventsByUser<T>(string userId);

        GoingNotGoingViewModel AddGoingUser(string id, int eventId);

        GoingNotGoingViewModel AddNotGoingUser(string id, int eventId);

        IEnumerable<T> GetSearchedEvents<T>(EventSearchModel model);

        Task UpdateInfo(EventViewModel model);

        Task DeleteEventAsync(int id);

        IEnumerable<T> GetMostCommentedEvents<T>();

        IEnumerable<TopEventsByGoingUsers> GetMostVisitedEvents();
    }
}
