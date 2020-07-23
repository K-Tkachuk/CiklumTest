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
using AutoMapper;
using Microsoft.OpenApi.Models;
using CiklumTest.Helpers;

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
            ConfigureSettings<Settings>(services);
            services.RegisterServices();
            services.AddDbContext<CiklumDbContext>(opt => opt.UseInMemoryDatabase());
            services.AddMvc();
            services.AddAutoMapper(typeof(AutoMapping));

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
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "CiklumTest API",
                    Description = "A sample API for testing and prototyping CiklumTest features"
				});
                c.OperationFilter<AddAuthHeaderOperationFilter>();
                c.AddSecurityDefinition("Authorization", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "bearer"

                });

                OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference()
                    {
                        Id = "Authorization",
                        Type = ReferenceType.SecurityScheme
                    }
                };
                OpenApiSecurityRequirement securityRequirements = new OpenApiSecurityRequirement()
                {
                    {securityScheme, new string[] { }},
                };


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

            app.UseAuthentication();


            string folder = Directory.GetCurrentDirectory() + "//Logs";
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            loggerFactory.AddFile(Path.Combine(folder, $"logger-{DateTime.Today:dd-MM-yyyy}.txt"));
            var logger = loggerFactory.CreateLogger("FileLogger");

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseMvc();
			

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
