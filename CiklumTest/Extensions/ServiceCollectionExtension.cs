using System;
using CiklumTest.Services;
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

            return services;
        }
    }
}
