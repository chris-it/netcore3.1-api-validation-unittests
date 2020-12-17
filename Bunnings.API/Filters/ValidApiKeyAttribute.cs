using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Bunnings.API.Filters
{
    /// <summary>
    /// This a custom filter to validate incoming requests. In this example, we are validating if the requst has a specific header key.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ValidApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        private const string apiKeyHeaderName = "x-api-key";

        // Note: This Key can come from the configuration or a sectets vault 
        private const string validApiKey = "bunnings";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(apiKeyHeaderName, out var apiKeyValue))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (apiKeyValue != validApiKey)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
        }
    }
}
