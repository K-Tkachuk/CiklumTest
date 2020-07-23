using System;
using CiklumTest.Repositories;
using CiklumTest.Repositories.Interfaces;
using CiklumTest.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CiklumTest.Extensions
{
	public static class ServiceCollectionExtension
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IToDoService, ToDoService>();
			services.AddScoped<IUserService, UserService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IToDoRepository, ToDoRepository>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return services;
        }
    }
}
