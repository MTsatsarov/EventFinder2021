namespace EventFinder2021.Web.ViewModels.EventViewModels
{
    using EventFinder2021.Data.Models.Enums;

    public class EventSearchModel
    {
        public string Name { get; set; }

        public City City { get; set; }

        public Category Category { get; set; }
    }
}
