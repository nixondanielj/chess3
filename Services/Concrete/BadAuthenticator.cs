using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Concrete
{
    public class BadAuthenticator : IAuthorizationService
    {
        public bool Authenticate(string key)
        {
            throw new NotImplementedException();
        }

        public string Authenticate(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
