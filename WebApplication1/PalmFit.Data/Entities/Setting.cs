namespace Palmfit.Data.Entities
{
    public class Setting : BaseEntity
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}