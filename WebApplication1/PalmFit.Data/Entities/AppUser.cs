using Palmfit.Data.EntityEnums;
using System.Collections.Generic;
using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Palmfit.Data.Entities
{
    public class AppUser : IdentityUser
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public string Area { get; set; }
        public string State { get; set; }
        public Gender? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Country { get; set; }
        public bool? IsLockedOut { get; set; }
        public DateTime? LastOnline { get; set; }
        public bool? IsVerified { get; set; }
        public bool? IsArchived { get; set; }
        public bool? Active { get; set; }
        public string ReferralCode { get; set; }
        public string InviteCode { get; set; }
        public Health Health { get; set; }
        public Setting Setting { get; set; }
        public Wallet Wallet { get; set; }

        public ICollection<Invite> Invities { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Subscription> Subscriptions { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}