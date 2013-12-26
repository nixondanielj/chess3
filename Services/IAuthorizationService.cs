using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IAuthorizationService
    {
        public bool Authenticate(string key);
        public string Authenticate(string email, string password);
    }
}
