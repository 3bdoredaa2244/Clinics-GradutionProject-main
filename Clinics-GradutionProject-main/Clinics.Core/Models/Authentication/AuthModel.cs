﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.Core.Models.Authentication
{
    public class AuthModel
    {
        public string UserId { get; set; }
        public string Message { get; set; }
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
