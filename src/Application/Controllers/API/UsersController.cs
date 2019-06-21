using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using DataTransferObjects.Entities.Identity;
using Entities.Permissions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Application.Controllers.API
{
    [Route("api/users")]
    public class UsersController : Controller
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var users = await this.userService.GetAllUsersAsync();

            return Ok(users);
        }

        [HttpGet]
        [Route("current")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCurrentUser()
        {
            var idClaim = User.Claims.Where(x => x.Type == JwtRegisteredClaimNames.Jti).FirstOrDefault();
            if (idClaim != null)
            {
                Guid userId = Guid.Parse(idClaim.Value);
                var user = await this.userService.GetUserAsync(userId);

                return Ok(user);
            }

            return BadRequest();

        }
    }
}
