# ASP.NET Core 6.0 integration testing

## User API

- Web API to perform users CRUD operations
- Tree layer architecture

### Technologies and libraries:

- API layer build with [ASP.NET Core 6.0](https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-6.0?view=aspnetcore-6.0)
- Swagger documentation with [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
- MS SQL database
- Unit test with [xUnit](https://github.com/xunit/xunit)
- Integration tests with [xUnit](https://github.com/xunit/xunit) and [TestContainers](https://github.com/testcontainers/testcontainers-dotnet)

## Order API

- Web API to manage orders
- Use User API to get information about user who create order
- Tree layer architecture (with hardcoded data access layer)

### Technologies and libraries:

- API layer build with [ASP.NET Core 6.0](https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-6.0?view=aspnetcore-6.0)
- Swagger documentation with [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
- Integration tests with [xUnit](https://github.com/xunit/xunit) and [WireMock](https://github.com/WireMock-Net/WireMock.Net)
