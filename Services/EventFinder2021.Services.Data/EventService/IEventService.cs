namespace EventFinder2021.Services.Data.EventService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EventFinder2021.Web.ViewModels.EventViewModels;

    public interface IEventService
    {
        Task CreateEventAsync(CreateEventInputModel model, string imagePath);

        IEnumerable<EventViewModel> GetAllEvents(int pageNumber, int itemsPerPage = 12);

        EventViewModel GetEventById(int id);

        int GetCount();

        IEnumerable<EventViewModel> GetEventsByUser(string userId);

        int AddGoingUser(string id, int eventId);

        int AddNotGoingUserAsync(string id, int eventId);

        IEnumerable<EventViewModel> GetSearchedEvents(EventSearchModel model);

        Task UpdateInfo(EventViewModel model);
    }
}
