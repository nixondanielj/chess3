﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class Settings
    {
        public TimeSpan GetAuthExpiration()
        {
            return new TimeSpan(7, 0, 0, 0);
        }
    }
}
