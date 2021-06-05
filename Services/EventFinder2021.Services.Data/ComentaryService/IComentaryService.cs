﻿namespace EventFinder2021.Services.Data.ComentaryService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EventFinder2021.Services.Models;
    using EventFinder2021.Web.ViewModels.ComentaryModels;

    public interface IComentaryService
    {
        Task WriteComentary(PostComentaryModel model);

        public IEnumerable<ComentaryViewModel> GetAllEventComentaries(int eventId);

        Task WriteReply(PostReplyModel model);

        public int GetComentaryCount(int eventId);
    }
}
