using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;
using DataAccess;
using Services.Models;
using Utility;

namespace Services.Concrete
{
    public class DefaultAuthService : IAuthorizationService
    {
        public AuthenticationVM Authenticate(AuthenticationFM credentials)
        {
            AuthenticationVM vm = null;
            if (credentials.Key != null)
            {
                vm = AuthByKey(credentials.Key);
            }
            else if (credentials.Email != null && credentials.Password != null)
            {
                vm = AuthByLogin(credentials.Email, credentials.Password);
            }
            return vm;
        }

        private AuthenticationVM AuthByLogin(string email, string password)
        {
            AuthenticationVM vm = null;
            using (var uow = new UnitOfWork())
            {
                var user = uow.UserRepository.GetByEmail(email)
                    .SingleOrDefault(u=>u.Password == password);
                if (user != null)
                {
                    Token token = CreateToken(user);
                    vm = new AuthenticationVM();
                    vm.Key = token.Key;
                    vm.UserId = user.Id;
                }
            }
            return vm;
        }

        private Token CreateToken(User user)
        {
            Token token = new Token();
            token.Key = Guid.NewGuid().ToString();
            token.UserId = user.Id;
            using (var uow = new UnitOfWork())
            {
                uow.TokenRepository.Create(token);
            }
            return token;
        }

        private AuthenticationVM AuthByKey(string key)
        {
            AuthenticationVM vm = null;
            using (var uow = new UnitOfWork())
            {
                Token token = uow.TokenRepository.GetByKey(key).SingleOrDefault();
                if (token != null)
                {
                    if ((DateTime.Now - token.Issued) > Settings.GetAuthExpiration())
                    {
                        throw new ExpiredAuthenticationException();
                    }
                    else
                    {
                        vm = new AuthenticationVM();
                        vm.Key = token.Key;
                        vm.UserId = token.UserId;
                    }
                }
            }
            return vm;
        }
    }
}
