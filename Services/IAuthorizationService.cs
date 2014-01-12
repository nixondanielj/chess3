using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IAuthorizationService
    {
        public AuthenticationVM Authenticate(string key);
        public AuthenticationVM Authenticate(string email, string password);
    }
}
