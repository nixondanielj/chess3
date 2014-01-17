﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public class AuthenticationFM
    {
        public string Key { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public AuthenticationFM()
        {

        }

        public AuthenticationFM(string key)
        {
            this.Key = key;
        }
    }
}
