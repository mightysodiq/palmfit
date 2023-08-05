﻿using Palmfit.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class UserOTP : BaseEntity
    {
        public string Email { get; set; }
        public string OTP { get; set; }
        public DateTime Expiration { get; set; }
    }


}
