namespace EventFinder2021.Web.ViewModels.EventViewModels
{
    using EventFinder2021.Data.Models;
    using EventFinder2021.Services.Mapping;

    public class EventViewModel : IMapFrom<Event>, IMapTo<Event>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string City { get; set; }

        public string Category { get; set; }

        public string ImageUrl { get; set; }

        public string Date { get; set; }

        public string CreatorId { get; set; }
    }
}
