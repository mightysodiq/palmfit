using Palmfit.Data.EntityEnums;
using System;

namespace Palmfit.Data.Entities
{
    public class Subscription : BaseEntity
    {
        public SubscriptionType Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsExpired { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}