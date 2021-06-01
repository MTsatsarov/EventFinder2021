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

        public List<EventViewModel> GetAllEvents(string path)
        {
            var models = new List<EventViewModel>();
            var events = this.db.Events.ToList();
            foreach (var currEvent in events)
            {
                var extension = this.db.Images.Where(x => x.Id == currEvent.ImageId).Select(x => x.Extension).FirstOrDefault();
                var imageUrl ="/images/Events/" + currEvent.ImageId + "." + extension;
                var viewModel = new EventViewModel()
                {
                    Name = currEvent.Name,
                    Description = currEvent.Description,
                    Category = currEvent.Type.ToString(),
                    City = currEvent.City.ToString(),
                    ImageUrl = imageUrl,
                };
                models.Add(viewModel);
            }

            return models;
        }
    }
}
