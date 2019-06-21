# Unit & Integration Tests
<br/>
Setup Test Project
![Alt text](/lectures/assets/images/test.jpg "xUnit")
---
<br/>
Dependencies
```
<PackageReference Include="AutoMapper" Version="8.1.1" />
<PackageReference Include="Microsoft.AspNetCore.Mvc" Version="2.2.0" />
<PackageReference Include="Microsoft.AspNetCore.TestHost" Version="2.2.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.InMemory" Version="2.2.4" />
<PackageReference Include="Microsoft.NET.Test.Sdk" Version="16.0.1" />
<PackageReference Include="xunit" Version="2.4.0" />
<PackageReference Include="xunit.runner.visualstudio" Version="2.4.0" />
```
<br/>
Create Test Web Server
```
public class TestServerFixture : IDisposable
{
    public TestServer TestServer { get; set; }

    public TestServerFixture()
    {
        var hostBuilder = new WebHostBuilder()
                .UseEnvironment("Testing")
                .UseStartup<Startup>();
        TestServer = new TestServer(hostBuilder);
    }

    public void Dispose()
    {
        TestServer.Dispose();
    }
}
```
<br/>
Setup Startup Configuration
```
public void ConfigureServices(IServiceCollection services)
{
    services.AddDbContext<EntityContext>(options => options.UseInMemoryDatabase(databaseName: "VueNetDatabase"));

    ...

}
```
<br/>
Create Abstract Tests Class
```
public abstract class AbstractTestsConstructor
{
    private readonly TestServerFixture testServerFixture = new TestServerFixture();

    public TService GetService<TService>() where TService : class
    {
        return this.testServerFixture.TestServer?.Host?.Services?.GetService(typeof(TService)) as TService;
    }
}
```
<br/>
Test it
```
public class IdentityServiceTests : AbstractTestsConstructor
{
    [Fact]
    public async Task RegisterUser_ValidData_CreateAnUser()
    {
        var identityService = GetService<IIdentityService>();

        var result = await identityService.CreateUserAsync(TestUser, "admin");

        Assert.True(result.Item1);
        Assert.NotNull(result.Item2);
    }
}
```