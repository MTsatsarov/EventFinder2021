namespace EventFinder2021.Data.Models
{
    using EventFinder2021.Data.Common.Models;

    public class Vote : BaseModel<int>
    {
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int EventId { get; set; }

        public virtual Event Event { get; set; }

        public byte Grade { get; set; }
    }
}
