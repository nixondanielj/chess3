using Services;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ChessAPI.Controllers
{
    public class LoginController : ApiController
    {
        private IAuthorizationService authService;
        private AuthHelper helper;
        public LoginController(IAuthorizationService authService, AuthHelper helper)
        {
            this.helper = helper;
            this.authService = authService;
        }
        // POST api/<controller>
        public void Post(AuthenticationFM credentials)
        {
            string key = authService.Authenticate(credentials).Key;
            var context = new HttpContextWrapper(HttpContext.Current);
            helper.SetKey(context.Request, context.Response, key);
        }
    }
}