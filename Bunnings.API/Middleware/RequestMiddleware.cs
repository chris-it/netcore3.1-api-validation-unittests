using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;

namespace Bunnings.API.Middleware
{
    /// <summary>
    /// This is an example Middleware that attaches a correlation ID to all requests in a single http request
    /// (could be useful in logging etc. in micro-service scenarios when multiple microservices are called during a single http request)
    /// </summary>
    public class RequestMiddleware
    {
        private readonly RequestDelegate _next;
        private const string correlationIdKeyName = "x-correlation-id";

        public RequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers[correlationIdKeyName].Count == 0)
                context.Request.Headers.Add(correlationIdKeyName, new StringValues(Guid.NewGuid().ToString()));

            await _next.Invoke(context);
        }
    }
}
