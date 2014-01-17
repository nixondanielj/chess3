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
    public class LoginController : BaseAPIController
    {
        private IAuthorizationService authService;
        private AuthHelper helper;
        public LoginController(IAuthorizationService authService, AuthHelper helper)
        {
            this.helper = helper;
            this.authService = authService;
        }

        // POST api/<controller>
        public HttpResponseMessage Post(AuthenticationFM credentials)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            var result = authService.Authenticate(credentials);
            if (result != null)
            {
                helper.SetKey(Request, Response, result.Key);
                response.StatusCode = HttpStatusCode.OK;
            }
            return response;
        }
    }
}