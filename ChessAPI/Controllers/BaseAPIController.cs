using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ChessAPI.Controllers
{
    public class BaseAPIController : ApiController
    {
        public HttpRequestBase Request { get; set; }
        public HttpResponseBase Response { get; set; }

        public BaseAPIController()
        {
            var context = new HttpContextWrapper(HttpContext.Current);
            this.Request = context.Request;
            this.Response = context.Response;
        }
    }
}