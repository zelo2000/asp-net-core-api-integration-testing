using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OZ.OrderApi.Services.Orders;
using OZ.OrderApi.Services.Users;

namespace OZ.OrderApi.Services
{
    public static class ServiceModule
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IUserApiClient, UserApiClient>(client =>
            {
                var baseUrl = configuration["UserApi:BaseUrl"];
                client.BaseAddress = new Uri(baseUrl);
            });

            services.AddTransient<IOrderService, OrderService>();
            return services;
        }
    }
}
