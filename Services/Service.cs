using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityAbstractions;

namespace Services
{
    abstract class Service
    {
        protected ILogger Log { get; set; }

        public Service(ILogger log)
        {
            this.Log = log;
        }
    }
}
