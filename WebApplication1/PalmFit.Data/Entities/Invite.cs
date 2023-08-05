using System;

namespace Palmfit.Data.Entities
{
    public class Invite : BaseEntity
    {
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}