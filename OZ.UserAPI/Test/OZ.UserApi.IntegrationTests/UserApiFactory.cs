using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OZ.UserApi.Data;
using OZ.UserApi.WebApi;
using Testcontainers.MsSql;

namespace OZ.UserApi.IntegrationTests
{
    public class UserApiFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
    {
        private readonly MsSqlContainer _container = new MsSqlBuilder()
            .Build();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(context =>
            {
                context.RemoveAll<DbContextOptions<UserApiDbContext>>();
                context.RemoveAll<UserApiDbContext>();
                context.AddDbContext<UserApiDbContext>(opt => opt.UseSqlServer(_container.GetConnectionString()));
            });
        }

        public async Task InitializeAsync()
        {
            await _container.StartAsync();
        }

        async Task IAsyncLifetime.DisposeAsync()
        {
            await _container.StopAsync();
        }
    }
}
