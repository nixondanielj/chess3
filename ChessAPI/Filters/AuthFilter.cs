using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ChessAPI.Filters
{
    public class AuthorizationFilter : IActionFilter
    {

        public Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {

        }

        public bool AllowMultiple
        {
            get { throw new NotImplementedException(); }
        }
    }
}