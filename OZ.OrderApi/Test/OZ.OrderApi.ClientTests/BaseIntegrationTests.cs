using OZ.OrderApi.Client;

namespace OZ.OrderApi.ClientTests
{
    public abstract class BaseIntegrationTests
    {
        private const string Url = "http://localhost:5100/";
        protected readonly OrderApiClient Client;

        protected BaseIntegrationTests()
        {
            Client = new OrderApiClient(Url);
        }
    }
}
