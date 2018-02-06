﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Core2WebApi.Entities.Session
{
    public class SessionUserModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string UserName { get; set; }
        public List<SessionUserClaimModel> UserClaims { get; set; }
        //public List<Claim> RoleClaims { get; set; }
    }
}