using Palmfit.Data.EntityEnums;
using System;

namespace Palmfit.Data.Entities
{
    public class WalletHistory : BaseEntity
    {
        public decimal Amount { get; set; }
        public WalletType Type { get; set; } // Is this a subscription Type
        public DateTime Date { get; set; }
        public string Reference { get; set; }
        public string Details { get; set; }
        public string WalletAppUserId { get; set; }
        public Wallet Wallet { get; set; }
    }
}