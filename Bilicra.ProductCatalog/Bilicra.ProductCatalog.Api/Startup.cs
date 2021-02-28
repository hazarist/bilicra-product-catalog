using AutoMapper;
using Bilicra.ProductCatalog.Api.Middlewares;
using Bilicra.ProductCatalog.Business.Interfaces;
using Bilicra.ProductCatalog.Business.Services;
using Bilicra.ProductCatalog.Common.Settings;
using Bilicra.ProductCatalog.DataAccess.Context;
using Bilicra.ProductCatalog.DataAccess.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Text.Json;

namespace Bilicra.ProductCatalog.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true);

            Configuration = builder.Build();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddSingleton(cfg => cfg.GetService<IOptions<AppSettings>>().Value);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionResultExecutor<ObjectResult>, ResponseWrapperMiddleware>();

            //db connection
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("PostgreSqlConnection")));

            //Dependency Injection
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddAutoMapper();

            services.AddMvc(opt =>
            {
                opt.Filters.Add(new AuthorizeFilter());
            }).AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase);

            services.AddHealthChecks();

           

            var settings = Configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(settings.SigningKey);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
          .AddJwtBearer(x =>
          {
              x.RequireHttpsMetadata = false;
              x.SaveToken = true;
              x.TokenValidationParameters = new TokenValidationParameters
              {
                  ValidateIssuerSigningKey = true,
                  IssuerSigningKey = new SymmetricSecurityKey(key),
                  ValidateIssuer = false,
                  ValidateAudience = false
              };
          });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/hc");
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Project is healthy");
                });
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
