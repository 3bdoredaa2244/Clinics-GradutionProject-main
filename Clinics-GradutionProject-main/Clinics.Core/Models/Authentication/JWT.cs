﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.Core.Models.Authentication
{
    public class JWT
    {
        public string key { get; set; }
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public double DurationInDays { get; set; }

    }
}
