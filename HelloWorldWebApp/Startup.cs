// <copyright file="Startup.cs" company="Principal 33">
// Copyright (c) Principal 33. All rights reserved.
// </copyright>

using System;
using System.IO;
using System.Reflection;
using HelloWorldWebApp.Controllers;
using HelloWorldWebApp.Data;
using HelloWorldWebApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace HelloWorldWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
            services.AddScoped<ITeamService, DbTeamService>();
            services.AddSingleton<ITimeService, TimeService>();
            services.AddSingleton<IBroadcastService, BroadcastService>();
            services.AddSingleton<IWeatherConfigurationSettings, WeatherConfigurationSettings>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hello World API", Version = "v1" });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
            });
            string databaseURL = Environment.GetEnvironmentVariable("DATABASE_URL");
            databaseURL = databaseURL != null ? ConvertHerokuStringToASPString(databaseURL) : Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(databaseURL));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();

            AssignRoleProgramaticaly(services.BuildServiceProvider());
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapHub<MessageHub>("/messagehub");
                endpoints.MapRazorPages();
            });

            
        }

        private async void AssignRoleProgramaticaly(IServiceProvider services)
        {
            UserManager<IdentityUser> userManager = services.GetRequiredService<UserManager<IdentityUser>>();
            IdentityUser user = await userManager.FindByNameAsync("george@gmail.com");
            await userManager.AddToRoleAsync(user, "Administrators");
        }

        public static string ConvertHerokuStringToASPString(string herokuConnectionString)
        {
            var databaseUri = new Uri(herokuConnectionString);
            string[] userInfo = databaseUri.UserInfo.Split(':');

            int port = databaseUri.Port;
            string host = databaseUri.Host;
            string userId = userInfo[0];
            string password = userInfo[1];
            string database = databaseUri.AbsolutePath[1..];

            string result = $"Host={host};Port={port};Database={database};User Id={userId};Password={password};Pooling=true;SSL Mode=Require;TrustServerCertificate=True;Include Error Detail=True";
            return result;
        }
    }
}
