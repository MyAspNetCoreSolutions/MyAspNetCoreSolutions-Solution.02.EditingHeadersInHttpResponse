using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Tamrin
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class LotfiResponseEditingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfigurationRoot _configurationRoot;

        public LotfiResponseEditingMiddleware(RequestDelegate next, IConfigurationRoot confiqurationRoot)
        {
            _next = next;
            _configurationRoot= confiqurationRoot;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            await _next(httpContext);

            var headerSection = _configurationRoot.GetSection("MySection");
            string s = "";
            foreach (var header in headerSection.GetChildren())
            {
               
                    httpContext.Response.Headers.Add(header.Key, header.Value);
                s += "   " + header.Key + " - > " + header.Value;
            }
            await httpContext.Response.WriteAsync(s);


        }
    }  
}
