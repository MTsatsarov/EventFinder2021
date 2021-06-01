namespace EventFinder2021.Web.ViewModels.EventViewModels
{
    using System;
    using System.Collections.Generic;

    public class ListEventViewModel
    {
        public IEnumerable<EventViewModel> Events { get; set; }

        public int PageNumber { get; set; }

        public int RecipeCount { get; set; }

        public int ItemsPerPage { get; set; }

        public int PagesCount => (int)Math.Ceiling((double)this.RecipeCount / this.ItemsPerPage);

        public bool HasPreviousPage => this.PageNumber > 1;

        public int PreviousPageNumber => this.PageNumber - 1;

        public bool HasNextPage => this.PageNumber < this.PagesCount;

        public int NextPage => this.PageNumber + 1;
    }
}
