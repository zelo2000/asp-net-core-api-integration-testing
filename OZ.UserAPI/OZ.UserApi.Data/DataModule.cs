using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OZ.UserApi.Data.Users;

namespace OZ.UserApi.Data
{
    public static class DataModule
    {
        public static IServiceCollection ConfigureData(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("UserApiDatabase");
            services.AddDbContext<UserApiDbContext>(options => options.UseSqlServer(connectionString, x => x.MigrationsAssembly("OZ.UserApi.Data")));

            services.AddTransient<IUserRepository, UserRepository>();

            return services;
        }
    }
}
