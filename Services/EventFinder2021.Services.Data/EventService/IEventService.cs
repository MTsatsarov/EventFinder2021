namespace EventFinder2021.Services.Data.EventService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EventFinder2021.Web.ViewModels.EventViewModels;

    public interface IEventService
    {
        Task CreateEventAsync(CreateEventInputModel model, string imagePath);

        List<EventViewModel> GetAllEvents(string path);
    }
}
