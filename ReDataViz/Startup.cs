using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ReDataViz
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApiContext>(option => option.UseInMemoryDatabase("MyLocalDB1"));

            services.AddCors( options => options.AddPolicy("Cors",
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    }
                ));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseCors("Cors");
            app.UseMvc();

            SeedData(serviceProvider.GetService<ApiContext>()); // Added service provider
        }

        public void SeedData(ApiContext _context)
        {
            _context.DtvizMessages.Add(new Models.DtvizMessage
            {
                Owner = "John",
                Text = "What a sunny day"
            });
            _context.DtvizMessages.Add(new Models.DtvizMessage
            {
                Owner = "Jane",
                Text = "Indeed it is"
            });
            _context.Users.Add(new Models.User {
                FirstName = "Test",
                LastName = "User",
                EmailID = "tu@test.com",
                Passsword = "pwd"
    });


 
            _context.SaveChanges();
        }
    }
}
