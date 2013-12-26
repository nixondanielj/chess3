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
        protected UnitOfWork UOW { get; set; }

        public Service(ILogger log, UnitOfWork uow)
        {
            this.Log = log;
            this.UOW = uow;
        }
    }
}
