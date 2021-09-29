namespace EventFinder2021.Web.Hubs
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EventFinder2021.Services.Data.ComentaryService;
    using EventFinder2021.Web.ViewModels.ComentaryModels;
    using Microsoft.AspNetCore.SignalR;

    public class EventViewHub : Hub
    {
        private static readonly HashSet<string> ListOfGroups = new();
        private readonly IComentaryService comentaryService;

        public EventViewHub(IComentaryService comentaryService)
        {
            this.comentaryService = comentaryService;
        }
        public async Task AddUserToGroup(string groupId)
        {
            var currentConnection = this.Context.ConnectionId;
            if (!ListOfGroups.Contains(groupId))
            {
                ListOfGroups.Add(groupId);
            }

            await this.Groups.AddToGroupAsync(currentConnection, groupId);
        }

        public async Task RemoveUserFromGroup(string groupId)
        {
            var currentConnection = this.Context.ConnectionId;
            await this.Groups.RemoveFromGroupAsync(currentConnection, groupId);
        }

        public async override Task OnConnectedAsync()
        {
            await this.Clients.Caller.SendAsync("GetGroup", "add");
        }

        public async override Task OnDisconnectedAsync(System.Exception exception)
        {
            await this.Clients.Caller.SendAsync("GetGroup", "remove");
        }

        public async Task DisplayNewCommentToUsers()
        {
            var comentary = this.comentaryService.ReturnLastAddedComment();
            var group = comentary.EventId.ToString();
            await this.Clients.Group(group).SendAsync("DisplayNewComment", comentary);
        }
    }
}
