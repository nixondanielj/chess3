using Services;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace ChessAPI.Filters
{
    public class AuthFilter : AuthorizeAttribute
    {
        private IAuthorizationService authService;

        public AuthFilter(IAuthorizationService authService)
            : base()
        {
            this.authService = authService;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var context = new HttpContextWrapper(HttpContext.Current);
            string key = new AuthHelper().GetKey(context.Request);
            if (key == null || authService.Authenticate(new AuthenticationFM(key)) == null)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.Forbidden);
            }
        }
    }
}