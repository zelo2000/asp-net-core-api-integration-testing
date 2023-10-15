using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using OZ.UserApi.Services.Images;
using OZ.UserApi.Services.Users;

namespace OZ.UserApi.Services
{
    public static class ServiceModule
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<IValidatorMarker>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IStorageService, StorageService>();

            return services;
        }
    }
}
