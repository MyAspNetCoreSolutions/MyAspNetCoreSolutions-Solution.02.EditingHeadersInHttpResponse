using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Tamrin
{
    public class Startup
    {
        IConfigurationRoot configuration;

        public Startup(IHostingEnvironment env)
        {
            var Builder = new ConfigurationBuilder().SetBasePath(env.ContentRootPath).AddJsonFile("appsettings.json");
            configuration = Builder.Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(typeof(IConfigurationRoot), configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseMiddleware<LotfiResponseEditingMiddleware>();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // وقتی از خط زیر استفاده میکنم این خطا رو می گیرم
            // An unhandled exception was thrown by the application.
            // System.InvalidOperationException: Headers are read - only, response has already started.

            app.Run(async (context) =>
            {
                //await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
