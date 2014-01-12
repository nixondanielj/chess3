using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Services.Models;
using System.Net.Http;
using System.Web.Http;
using System.Net;
using Services;
using System.Net.Http.Headers;

namespace ChessAPI
{
    public class AuthHelper
    {
        private readonly IAuthorizationService authService;

        public AuthHelper(IAuthorizationService authService)
        {
            this.authService = authService;
        }

        public AuthenticationVM Authenticate(HttpRequestMessage request)
        {
            AuthenticationVM user = null;
            string sessionId = GetSessionId(request);
            if (sessionId != null)
            {
                user = authService.Authenticate(new AuthenticationFM() { Key = sessionId });
            }
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }
            return user;
        }

        public string GetSessionId(HttpRequestMessage request)
        {
            string sessionId = null;
            var cookie = request.Headers.GetCookies("sessionId").FirstOrDefault();
            if (cookie != null)
            {
                sessionId = cookie["sessionId"].Value;
            }
            return sessionId;
        }

        public void SetSessionCookie(HttpRequestMessage request, HttpResponseMessage response, string sessionKey)
        {
            var cookie = new CookieHeaderValue("sessionId", sessionKey);
            cookie.Expires = DateTimeOffset.Now.AddMinutes(15);
            cookie.Path = "/";
            response.Headers.AddCookies(new CookieHeaderValue[] { cookie });
        }
    }
}