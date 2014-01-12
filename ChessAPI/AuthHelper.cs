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
        public string GetKey(HttpRequestBase request)
        {
            string sessionId = null;
            var cookie = request.Cookies["sessionId"];
            if (cookie != null)
            {
                sessionId = cookie.Value;
            }
            return sessionId;
        }

        public void SetKey(HttpRequestBase request, HttpResponseBase response, string sessionKey)
        {
            var cookie = new HttpCookie("sessionId", sessionKey);
            cookie.Expires = DateTime.Now.AddMinutes(15);
            cookie.Path = "/";
            response.SetCookie(cookie);
        }
    }
}