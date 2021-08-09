namespace EventFinder2021.Services.Data.EventService
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using EventFinder2021.Common;
    using EventFinder2021.Data;
    using EventFinder2021.Data.Models;
    using EventFinder2021.Services.Data.VoteService;
    using EventFinder2021.Services.Mapping;
    using EventFinder2021.Web.ViewModels.EventViewModels;

    public class EventService : IEventService
    {
        private readonly ApplicationDbContext db;
        private readonly IVoteService voteService;

        public EventService(ApplicationDbContext db, IVoteService voteService)
        {
            this.db = db;
            this.voteService = voteService;
        }

        public GoingNotGoingViewModel AddGoingUser(string userId, int eventId)
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
                return new GoingNotGoingViewModel()
                {
                    GoingUsersCount = currEvent.GoingUsers.Users.Count(),
                    NotGoingUsersCount = currEvent.NotGoingUsers.Users.Count(),
                };
            }

            var notGoingUser = currEvent.NotGoingUsers.Users.Where(x => x.Id == user.Id).FirstOrDefault();
            if (notGoingUser != null)
            {
                currEvent.NotGoingUsers.Users.Remove(user);
            }

            this.db.Events.Where(x => x.Id == currEvent.Id).FirstOrDefault().GoingUsers.Users.Add(user);
            this.db.SaveChanges();

            return new GoingNotGoingViewModel()
            {
                GoingUsersCount = currEvent.GoingUsers.Users.Count(),
                NotGoingUsersCount = currEvent.NotGoingUsers.Users.Count(),
            };
        }

        public GoingNotGoingViewModel AddNotGoingUser(string userId, int eventId)
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

            if (currEvent.NotGoingUsers.Users.Any(x => x.Id == user.Id))
            {
                return new GoingNotGoingViewModel()
                {
                    GoingUsersCount = currEvent.GoingUsers.Users.Count(),
                    NotGoingUsersCount = currEvent.NotGoingUsers.Users.Count(),
                };
            }

            if (currEvent.GoingUsers.Users.Contains(user))
            {
                currEvent.GoingUsers.Users.Remove(user);
            }

            currEvent.NotGoingUsers.Users.Add(user);
            this.db.Events.Where(x => x.Id == currEvent.Id).FirstOrDefault().NotGoingUsers.Users.Add(user);
            this.db.SaveChanges();

            return new GoingNotGoingViewModel()
            {
                GoingUsersCount = currEvent.GoingUsers.Users.Count(),
                NotGoingUsersCount = currEvent.NotGoingUsers.Users.Count(),
            };
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

        public async Task DeleteEventAsync(int id)
        {
            var currentEvent = this.db.Events.Where(x => x.Id == id).FirstOrDefault();
            currentEvent.IsDeleted = true;
            currentEvent.DeletedOn = DateTime.UtcNow;
            this.db.Events.Update(currentEvent);
            await this.db.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllEvents<T>(int pageNumber, int itemsPerPage = 12)
        {
            var events = this.db.Events.OrderByDescending(x => x.Id).Skip((pageNumber - 1) * 12).Take(itemsPerPage).To<T>().ToList();
            return events;
        }

        public int GetCount()
        {
            return this.db.Events.Count();
        }

        public T GetEventById<T>(int id)
        {
            var currentEvent = this.db.Events.Where(x => x.Id == id).To<T>().FirstOrDefault();
            if (currentEvent == null)
            {
                throw new ArgumentException($"No event with Id:{id} was found");
            }

            return currentEvent;
        }

        public IEnumerable<T> GetEventsByUser<T>(string userId)
        {
            return this.db.Events.Where(x => x.UserId == userId).OrderByDescending(x => x.Date).To<T>().ToList();
        }

        public IEnumerable<T> GetMostCommentedEvents<T>()
        {
            var mostCommentedEvents = this.db.Events.OrderByDescending(x => x.Comentaries.Count()).To<T>().Take(10).ToList();
            return mostCommentedEvents;
        }

        public IEnumerable<T> GetMostVisitedEvents<T>()
        {
            var goingUsersCount = this.db.Events.OrderByDescending(x => x.GoingUsers.Users.Count).To<T>().Take(10).ToList();

            return goingUsersCount;
        }

        public IEnumerable<T> GetSearchedEvents<T>(EventSearchModel model)
        {
            var searchedCity = model.City;
            var searchedCategory = model.Category;
            var searchedName = model.Name;
            if (model.Name == null)
            {
                var events = this.db.Events.Where(x => x.City == searchedCity).Where(x => x.Category == searchedCategory).To<T>().ToList();
                return events;
            }

            var searchedEvents = this.db.Events.Where(x => x.City == searchedCity && x.Name == searchedName).Where(x => x.Category == searchedCategory).To<T>().ToList();

            return searchedEvents;
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
