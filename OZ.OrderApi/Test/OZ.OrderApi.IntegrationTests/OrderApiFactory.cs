using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OZ.OrderApi.WebApi;
using WireMock.Server;

namespace OZ.OrderApi.IntegrationTests
{
    public class OrderApiFactory : WebApplicationFactory<IApiMarker>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var wiremockServer = WireMockServer.Start();

            builder.ConfigureAppConfiguration(configurationBuilder =>
            {
                configurationBuilder.AddInMemoryCollection(new KeyValuePair<string, string>[]
                {
                    new ("UserApi:BaseUrl", wiremockServer.Urls[0])
                });
            });

            builder.ConfigureServices(collection =>
            {
                collection.AddSingleton(wiremockServer);
            });
        }
    }
}
