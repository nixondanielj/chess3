using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Concrete
{
    public class DefaultAuthService : IAuthorizationService
    {
        public int Authenticate(string key)
        {

        }

        public string Authenticate(string email, string password)
        {
            return "a";
        }
    }
}
