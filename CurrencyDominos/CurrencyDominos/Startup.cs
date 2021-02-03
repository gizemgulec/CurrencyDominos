using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrencyDominos.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyDominos
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton<ICurrencyServices, CurrencyServices>();
            services.AddSwaggerDocument(config=>{
                config.PostProcess = (doc => {
                    doc.Info.Title = "Currency Dominos";
                    doc.Info.Contact = new NSwag.OpenApiContact()
                    {
                        Url = "https://github.com/gizemgulec",
                        Email="gizem.gulec@bil.omu.edu.tr"
                    };
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseOpenApi();
            app.UseSwaggerUi3();
        }
    }
}
