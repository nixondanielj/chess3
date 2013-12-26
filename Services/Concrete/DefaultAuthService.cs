using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;
using DataAccess;
using Services.Models;

namespace Services.Concrete
{
    public class DefaultAuthService : IAuthorizationService
    {
        public AuthenticationVM Authenticate(string key)
        {
            using (var uow = new UnitOfWork())
            {
            }
        }

        public string Authenticate(AuthenticationVM model)
        {
            return "a";
        }
    }
}
