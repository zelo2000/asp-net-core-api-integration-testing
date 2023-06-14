# ASP.NET Core 6.0 integration testing

## User API

- Web API to perform users CRUD operations
- YAML for Azure DevOps pipelines
- Three-layer architecture

### Technologies and libraries:

- API layer build with [ASP.NET Core 6.0](https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-6.0?view=aspnetcore-6.0)
- Swagger documentation with [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
- API parameters validation using [FluentValidation](https://docs.fluentvalidation.net/en/latest/)
- MS SQL database
- Unit and integration tests with:
  - [xUnit](https://github.com/xunit/xunit)
  - [TestContainers](https://github.com/testcontainers/testcontainers-dotnet)
  - [AutoFixture](https://github.com/AutoFixture/AutoFixture)
  - [Shouldly](https://github.com/shouldly/shouldly)

## Order API

- Web API to manage orders
- Uses **_User API_** to get information about the user who created the order
- YAML for Azure DevOps pipelines
- Three-layer architecture (with hardcoded data access layer)

### Technologies and libraries:

- API layer build with [ASP.NET Core 6.0](https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-6.0?view=aspnetcore-6.0)
- Auto generated API client and swagger documentation with [NSwag](https://github.com/RicoSuter/NSwag)
- Integration and client tests with:
  - [xUnit](https://github.com/xunit/xunit)
  - [WireMock](https://github.com/WireMock-Net/WireMock.Net)
  - [AutoFixture](https://github.com/AutoFixture/AutoFixture)
  - [Shouldly](https://github.com/shouldly/shouldly)

## Result

### Order API

#### Test result

<img src="images/order_api_test_result.png" width="50%" height="auto" title="Order API tests" >

#### Pipeline build

<img src="images/order_api_pipeline_build_result.png" width="50%" height="auto" title="Order API build pipeline">

### User API

#### Test result

<img src="images/user_api_test_result.png" width="50%" height="auto" title="User API tests" >

#### Pipeline build

<img src="images/user_api_pipeline_build_result.png" width="50%" height="auto" title="User API build pipeline">

## References

- [Writing robust integration tests in .NET with WireMock.NET](https://www.youtube.com/watch?v=YU3ohofu6UU&ab_channel=NickChapsas)
- [The cleanest way to use Docker for testing in .NET](https://www.youtube.com/watch?v=8IRNC7qZBmk&ab_channel=NickChapsas)
- [Integration tests in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-6.0)
- [Automatically generating C# API clients on build with NSwag](https://blog.sanderaernouts.com/autogenerate-csharp-api-client-with-nswag)
- [Get started with NSwag and ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-nswag?view=aspnetcore-6.0&tabs=visual-studio)
