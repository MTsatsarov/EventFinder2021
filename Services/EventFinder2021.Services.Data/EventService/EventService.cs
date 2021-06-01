namespace EventFinder2021.Services.Data.EventService
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using EventFinder2021.Data;
    using EventFinder2021.Data.Models;
    using EventFinder2021.Web.ViewModels.EventViewModels;

    public class EventService : IEventService
    {
        private readonly ApplicationDbContext db;

        public EventService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task CreateEventAsync(CreateEventInputModel model, string imagePath)
        {
            Directory.CreateDirectory($"{imagePath}/images/Events/");
            var currEvent = new Event()
            {
                Name = model.Name,
                Description = model.Description,
                Date = model.Date,
                Type = model.Category,
                City = model.City
            };

            currEvent.User = this.db.Users.Where(x => x.UserName == model.CreatedByuser).FirstOrDefault();

            var extension = Path.GetExtension(model.Image.FileName).TrimStart('.');
            var image = new Image()
            {
                AddedByUser = currEvent.User,
                Event = currEvent,
                Extension = extension,
            };
            await this.db.Images.AddAsync(image);
            var physicalPath = $"{imagePath}/images/Events/{image.Id}.{extension}";
            currEvent.ImageId = image.Id;

            using (FileStream fileStream = new FileStream(physicalPath, FileMode.Create))
            {
                await model.Image.CopyToAsync(fileStream);
            }

            await this.db.Events.AddAsync(currEvent);

            await this.db.SaveChangesAsync();
        }

        public IEnumerable<EventViewModel> GetAllEvents(int pageNumber, int itemsPerPage = 12)
        {
            var events = this.db.Events.OrderByDescending(x => x.Id).Skip((pageNumber - 1) * 12).Take(itemsPerPage).Select(x => new EventViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Category = x.Type.ToString(),
                City = x.City.ToString(),
                Description = x.Description,
                ImageUrl = "/images/Events/" + x.ImageId + "." + x.Image.Extension,
            }).ToList();

            return events;
        }

        public int GetCount()
        {
            return this.db.Events.Count();
        }
    }
}
