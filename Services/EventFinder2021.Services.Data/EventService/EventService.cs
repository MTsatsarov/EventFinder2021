namespace EventFinder2021.Services.Data.EventService
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using EventFinder2021.Common;
    using EventFinder2021.Data;
    using EventFinder2021.Data.Common.Repositories;
    using EventFinder2021.Data.Models;
    using EventFinder2021.Data.Models.Enums;
    using EventFinder2021.Services.Data.VoteService;
    using EventFinder2021.Web.ViewModels.EventViewModels;

    public class EventService : IEventService
    {
        private readonly ApplicationDbContext db;
        private readonly EventFinder2021.Data.Common.Repositories.IDeletableEntityRepository<Image> imageRepository;
        private readonly IDeletableEntityRepository<Event> eventRepository;
        private readonly IVoteService voteService;

        public EventService(ApplicationDbContext db, IDeletableEntityRepository<Image> imageRepository, IDeletableEntityRepository<Event> eventRepository, IVoteService voteService)
        {
            this.db = db;
            this.imageRepository = imageRepository;
            this.eventRepository = eventRepository;
            this.voteService = voteService;
        }

        public int AddGoingUser(string userId, int eventId)
        {
            var currEvent = this.db.Events.Where(x => x.Id == eventId).FirstOrDefault();
            var user = this.db.Users.Where(x => x.Id == userId).FirstOrDefault();
            if (user == null)
            {
                throw new ArgumentNullException("User not found");
            }

            if (currEvent == null)
            {
                throw new InvalidOperationException("Event not found");
            }

            var goingUser = currEvent.GoingUsers.Users.Where(x => x.Id == user.Id).FirstOrDefault();
            if (goingUser != null)
            {
                throw new InvalidOperationException($"This user is already going to {currEvent.Name}");
            }

            var notGoingUser = currEvent.NotGoingUsers.Users.Where(x => x.Id == user.Id).FirstOrDefault();
            if (notGoingUser != null)
            {
                currEvent.NotGoingUsers.Users.Remove(user);
            }

            this.db.Events.Where(x => x.Id == currEvent.Id).FirstOrDefault().GoingUsers.Users.Add(user);
            this.db.SaveChanges();

            return currEvent.GoingUsers.Users.Count();
        }

        public int AddNotGoingUserAsync(string userId, int eventId)
        {
            var currEvent = this.db.Events.Where(x => x.Id == eventId).FirstOrDefault();
            var user = this.db.Users.Where(x => x.Id == userId).FirstOrDefault();
            if (user == null)
            {
                throw new ArgumentNullException("User not found");
            }

            if (currEvent == null)
            {
                throw new InvalidOperationException("Event not found");
            }

            if (!currEvent.NotGoingUsers.Users.Any(x => x.Id == user.Id))
            {
                throw new InvalidOperationException($"This user is already missing {currEvent.Name}");
            }

            if (currEvent.GoingUsers.Users.Contains(user))
            {
                currEvent.GoingUsers.Users.Remove(user);
            }

            currEvent.NotGoingUsers.Users.Add(user);
            this.db.Events.Where(x => x.Id == currEvent.Id).FirstOrDefault().NotGoingUsers.Users.Add(user);
            this.db.SaveChanges();

            return currEvent.NotGoingUsers.Users.Count();
        }

        public async Task CreateEventAsync(CreateEventInputModel model, string imagePath)
        {
            Directory.CreateDirectory($"{imagePath}/images/Events/");
            var currEvent = new Event()
            {
                Name = model.Name,
                Description = model.Description,
                Date = model.Date,
                Category = model.Category,
                City = model.City,
                GoingUsers = new GoingUsers(),
                NotGoingUsers = new NotGoingUsers(),
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
            currEvent.Image = image;
            using (FileStream fileStream = new FileStream(physicalPath, FileMode.Create))
            {
                await model.Image.CopyToAsync(fileStream);
            }

            await this.db.Events.AddAsync(currEvent);

            await this.db.SaveChangesAsync();
        }

        public async Task DeleteEvent(int id)
        {
            var currentEvent = this.db.Events.Where(x => x.Id == id).FirstOrDefault();
            this.eventRepository.Delete(currentEvent);
            await this.eventRepository.SaveChangesAsync();
        }

        public IEnumerable<EventViewModel> GetAllEvents(int pageNumber, int itemsPerPage = 12)
        {
            var events = this.db.Events.OrderByDescending(x => x.Id).Skip((pageNumber - 1) * 12).Take(itemsPerPage).Select(x => new EventViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Category = x.Category,
                City = x.City,
                Description = x.Description,
                ImageUrl = "/images/Events/" + x.ImageId + "." + x.Image.Extension ?? GlobalConstants.DefaultImageLocation,
                Date = x.Date,
                VotesAverageGrade = this.voteService.GetAverageVoteValue(x.Id),
            }).ToList();
            return events;
        }

        public int GetCount()
        {
            return this.db.Events.Count();
        }

        public EventViewModel GetEventById(int id)
        {
            var currentEvent = this.db.Events.Where(x => x.Id == id).FirstOrDefault();
            if (currentEvent == null)
            {
                throw new ArgumentException($"No event with Id:{id} was found");
            }

            var currImage = this.db.Images.Where(x => x.Id == currentEvent.ImageId).FirstOrDefault();

            var viewModel = new EventViewModel()
            {
                Id = currentEvent.Id,
                Name = currentEvent.Name,
                Category = currentEvent.Category,
                City = currentEvent.City,
                Description = currentEvent.Description,
                ImageUrl = "/images/Events/" + currentEvent.ImageId + "." + currentEvent.Image.Extension ?? GlobalConstants.DefaultImageLocation,
                Date = currentEvent.Date,
                CreatorId = currentEvent.UserId,
            };

            return viewModel;
        }

        public IEnumerable<EventViewModel> GetEventsByUser(string userId)
        {
            return this.db.Events.Where(x => x.UserId == userId).Select(x => new EventViewModel()
            {
                Name = x.Name,
                Description = x.Description,
                Id = x.Id,
                Date = x.Date,
                ImageUrl = "/images/Events/" + x.ImageId + "." + x.Image.Extension ?? GlobalConstants.DefaultImageLocation,
                City = x.City,
                Category = x.Category,
            }).OrderByDescending(x => x.Date).ToList();
        }

        public IEnumerable<EventViewModel> GetSearchedEvents(EventSearchModel model)
        {
            var searchedCity = model.City;
            var searchedCategory = model.Category;
            var searchedName = model.Name;
            List<EventViewModel> eventViewModels = new List<EventViewModel>();
            if (model.Name == null)
            {
                var events = this.db.Events.Where(x => x.City == searchedCity).Where(x => x.Category == searchedCategory).ToList();

                foreach (var currEvent in events)
                {
                    var eventViewModel = new EventViewModel()
                    {
                        Category = currEvent.Category,
                        City = currEvent.City,
                        Description = currEvent.Description,
                        ImageUrl = "/images/Events/" + currEvent.ImageId + "." + currEvent.Image.Extension ?? GlobalConstants.DefaultImageLocation,
                        CreatorId = currEvent.UserId,
                        Date = currEvent.Date,
                        Name = currEvent.Name,
                        Id = currEvent.Id,
                    };

                    eventViewModels.Add(eventViewModel);
                }

                return eventViewModels;
            }

            var searchedEvents = this.db.Events.Where(x => x.City == searchedCity && x.Name == searchedName).Where(x => x.Category == searchedCategory).ToList();

            foreach (var currEvent in searchedEvents)
            {
                var eventViewModel = new EventViewModel()
                {
                    Category = currEvent.Category,
                    City = currEvent.City,
                    Description = currEvent.Description,
                    ImageUrl = "/images/Events/" + currEvent.ImageId + "." + currEvent.Image.Extension ?? GlobalConstants.DefaultImageLocation,
                    CreatorId = currEvent.UserId,
                    Date = currEvent.Date,
                    Name = currEvent.Name,
                    Id = currEvent.Id,
                };

                eventViewModels.Add(eventViewModel);
            }

            return eventViewModels;
        }

        public async Task UpdateInfo(EventViewModel model)
        {
            var currEvent = this.db.Events.FirstOrDefault(x => x.Id == model.Id);

            currEvent.Name = model.Name;
            currEvent.Description = model.Description;
            currEvent.Date = model.Date;
            currEvent.City = model.City;
            currEvent.Category = model.Category;
            await this.db.SaveChangesAsync();
        }
    }
}
