using System.Threading.Tasks;
using DataTransferObjects.Entities.Identity;
using DataTransferObjects.HttpBodies.Request;
using DataTransferObjects.HttpBodies.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Services;

namespace Application.Controllers.API
{
    [Route("api/auth")]
    public class AuthenticationController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IIdentityService identityService;

        public AuthenticationController(IConfiguration configuration, IIdentityService identityService)
        {
            this.configuration = configuration;
            this.identityService = identityService;
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody]LoginRequest request)
        {
            if (User.Identity.IsAuthenticated)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }

            if (ModelState.IsValid)
            {
                LoginResponse loginResponse = await this.identityService.LoginUserAsync(request);

                return Ok(loginResponse);
            }

            return StatusCode(StatusCodes.Status400BadRequest);
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(typeof(RegisterResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Register([FromBody]RegisterUserDto request)
        {
            if (User.Identity.IsAuthenticated)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }

            if (ModelState.IsValid)
            {
                RegisterResponse response = new RegisterResponse();
                var registrationResultUser = await this.identityService.RegisterUserAsync(request);
                if (registrationResultUser != null)
                {
                    response.Success = true;
                    response.Message = "Registration success";
                }

                return Ok(response);
            }

            return StatusCode(StatusCodes.Status400BadRequest);
        }
    }
}
