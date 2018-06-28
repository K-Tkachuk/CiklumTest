using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CiklumTest.Extensions;
using CiklumTest.Models.DBModels;
using CiklumTest.Models.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using CiklumTest.Middleware;
using System.Linq;

namespace CiklumTest
{
    public class Startup
    {
		public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<CiklumDbContext>(opt => opt.UseInMemoryDatabase());
            services.AddMvc();
			ConfigureSettings<Settings>(services);
            services.RegisterServices();

            var settings = Configuration.GetSection("Settings").Get<Settings>();

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.IncludeErrorDetails = true;
                options.RequireHttpsMetadata = settings.AuthOptions.RequireHttpsMetadata;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = settings.AuthOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = settings.AuthOptions.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(settings.AuthOptions.LifeTime),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.AuthOptions.Key)),
                    ValidateIssuerSigningKey = true,
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "CiklumTest API",
                    Description = "A sample API for testing and prototyping CiklumTest features"
				});
			
				c.IncludeXmlComments("CiklumTest.WebApi.xml");
                c.DescribeAllEnumsAsStrings();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, CiklumDbContext context)
        {
            if (env.IsDevelopment())
            {
                CiklumDbInitializer.Initialize(context);
                app.UseDeveloperExceptionPage();
            }

            string folder = Directory.GetCurrentDirectory() + "//Logs";
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            loggerFactory.AddFile(Path.Combine(folder, $"logger-{DateTime.Today:dd-MM-yyyy}.txt"));
            var logger = loggerFactory.CreateLogger("FileLogger");

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseMvc();
			app.UseAuthentication();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }

		private static void ConfigureSettings<TSettings>(IServiceCollection services) where TSettings : class
        {
            services.Configure<TSettings>(Configuration);
            services.Configure<TSettings>(options => Configuration.GetSection(typeof(TSettings).Name).Bind(options));
        }
    }
}
