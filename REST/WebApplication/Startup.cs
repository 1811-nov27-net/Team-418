﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace WebApplication
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
            //< h2 class="stackerror">InvalidOperationException: 
            //The DbContextOptions passed to the IdentityDbContext 
            //constructor must be a DbContextOptions&lt;IdentityDbContext&gt;. 
            //When registering multiple DbContext types make sure that the constructor 
            //for each context type has a DbContextOptions&lt;TContext&gt; parameter 
            //rather than a non-generic DbContextOptions parameter.</h2>

            // Authorization database set service
            //services.AddDbContext<IdentityDbContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("AuthorizationDB")));
            

            services.AddScoped<IMusicRepo, MusicRepo>();

            services.AddDbContext<_1811proj1_5Context>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MusicDB")));


            //services
            //    .AddIdentity<IdentityUser, IdentityRole>(options =>
            //    {
            //        // Password settings (defaults - optional)
            //        options.Password.RequireDigit = true;
            //        options.Password.RequireLowercase = true;
            //        options.Password.RequireNonAlphanumeric = false;
            //        options.Password.RequireUppercase = false;
            //        options.Password.RequiredLength = 6;
            //        options.Password.RequiredUniqueChars = 1;
                    
            //    })
            //    .AddEntityFrameworkStores<IdentityDbContext>();

            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.Cookie.Name = "UserAuthentication";
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
            //    options.Events = new CookieAuthenticationEvents
            //    {
            //        OnRedirectToLogin = ctx =>
            //        {
            //            ctx.Response.StatusCode = 401; // Unauthorized (really, unauthenticated)
            //            return Task.FromResult(0);
            //        },
            //        OnRedirectToAccessDenied = ctx =>
            //        {
            //            ctx.Response.StatusCode = 403; // Forbidden (unauthorized, you're logged in, but you 
            //                                           // don't have the right role
            //            return Task.FromResult(0);
            //        },
            //    };
            //});

            services.AddAuthentication();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // TODO: Set up swagger
            // add Swashbuckle Swagger UI middleware
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new Info { Title = "TempRecord API", Version = "v1" });
            //});
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
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseAuthentication();

    // TODO: Set up swagger 
    //// Enable middleware to serve generated Swagger as a JSON endpoint.
    //app.UseSwagger();

    //// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
    //// specifying the Swagger JSON endpoint.
    //app.UseSwaggerUI(c =>
    //{
    //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TempRecord API v1");
    //});


    app.UseHttpsRedirection();
    app.UseMvc();
}
    }
}
