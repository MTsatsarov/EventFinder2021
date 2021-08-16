namespace EventFinder2021.Web.ViewModels.ReportModel
{
    using AutoMapper;
    using EventFinder2021.Data.Models;
    using EventFinder2021.Services.Mapping;
    using Newtonsoft.Json;

    public class EventReportViewModel : ReportViewModel, IMapFrom<Report>, IHaveCustomMappings
    {
        [JsonProperty("eventId")]
        public int? EventId { get; set; }

        [JsonProperty("reportId")]
        public int ReportId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Report, EventReportViewModel>()
                .ForMember(x => x.ReportedUserUsername, opt => opt.MapFrom(x => x.ReportedUserId))
                .ForMember(x => x.ReporterUserUsername, opt => opt.MapFrom(x => x.ReporterUserId))
                .ForMember(x => x.ReportId, opt => opt.MapFrom(x => x.Id));
        }
    }
}
