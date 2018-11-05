using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

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
                        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:4200");
                    }
                    
                ));
            services.AddSignalR();

            // This is for authentication
            var siginingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is the secret phrase"));

            services.AddAuthentication(opts =>
                {
                    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer( config =>
                {
                    config.RequireHttpsMetadata = false;
                    config.SaveToken = true;
                    config.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        // Set to false in mcking
                        IssuerSigningKey = siginingKey,
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = true
                    };
                });




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
            app.UseCors("Cors");
            
            //app.UseHttpsRedirection();
            
            

            app.UseSignalR((options) =>
            {
                options.MapHub<RtdHub>("/Notify");
            });
            app.UseAuthentication();
            app.UseMvc();
            SeedData(serviceProvider.GetService<ApiContext>()); // Added service provider
        }

        public void SeedData(ApiContext _context)
        {
            _context.DtvizMessages.Add(new Models.DtvizMessage
            {
                Owner = "Test",
                OwnerEmailID = "tu@test.com",
                Topic = "General",
                Text = "What a sunny day",
                Color = "red"
            });
            _context.DtvizMessages.Add(new Models.DtvizMessage
            {
                Owner = "Test2",
                OwnerEmailID = "tu2@test.com",
                Topic = "General",
                Text = "Indeed it is",
                Color = "orange"
            });
            _context.Users.Add(new Models.User {
                FirstName = "Test",
                LastName = "User",
                EmailID = "tu@test.com",
                Passsword = "pwd"
            });
            _context.Users.Add(new Models.User
            {
                FirstName = "Test2",
                LastName = "User2",
                EmailID = "tu2@test.com",
                Passsword = "pwd"
            });





            _context.SaveChanges();
        }
    }
}
