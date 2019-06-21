# Business & Application Layer
<br/>
### Services
![Alt text](/lectures/assets/images/services.jpg "Services")

#### Data Encapsulation
* Data Transfer Objects
* Auto Mapping
* Error Handling (total || defensive programming)
<br/>
#### Dependencies
* Unit Of Work
* Mapper
* Repositories
* External Services
* Configurations
<br/>
#### Abstraction
* Generic Types
* Reusable Code
<br/>
#### ! Important - Highly recommend is to prevent depending custom services on other custom services 
<br/>
#### Service Collection Extension
```
public static class ServicesConfigurationExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}
```
<br/>
### Controllers
#### Actions
```
public async Task<IActionResult> Get()
{
    // action logic
}
```
#### HTTP Method
* GET ```[HttpGet]```
* POST ```[HttpPost]```
* PUT ```[HttpPut]```
* DELETE ```[HttpDelete]```
#### Route
* Controller Route
* Action Route
#### Parameters
* From Body
* From Form
* From Query
* From Header
* From Route
* From Services
#### Dependencies
```
private readonly IConfiguration configuration;
private readonly IIdentityService identityService;

public AuthenticationController(IConfiguration configuration, IIdentityService identityService)
{
    this.configuration = configuration;
    this.identityService = identityService;
}
```
* Services
* Configurations
#### Authentication & Authorization
```
[Authorize(Roles = Roles.Admin, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
```
* Authentication Scheme
* Role-Based
* Policy-Based
#### Response Types & Status Codes
```
[ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
```
* On Success
* On Failure