﻿namespace EventFinder2021.Services.Data.ComentaryService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EventFinder2021.Web.ViewModels.ComentaryModels;

    public interface IComentaryService
    {
        Task WriteCommentaryAsync(RePostComentaryModel model);

        public IEnumerable<T> GetAllEventComentaries<T>(int eventId);

        public int GetComentaryCount(int eventId);
    }
}
