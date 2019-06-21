using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataTransferObjects.Entities.Identity;
using DataTransferObjects.HttpBodies.Request;
using DataTransferObjects.HttpBodies.Response;
using Entities.Identity;
using Entities.Permissions;
using GSK.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Services
{
    public class IdentityService : AbstractService, IIdentityService
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<Role> roleManager;
        private readonly IConfiguration configuration;

        public IdentityService(
            IUnitOfWork uow,
            IMapper mapper,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<Role> roleManager,
            IConfiguration configuration) : base(uow, mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
        }

        public async Task<Tuple<bool, string[]>> CreateRoleAsync(Role role, IEnumerable<string> claims)
        {
            if (claims == null)
                claims = new string[] { };

            string[] invalidClaims = claims.Where(c => ApplicationPermissions.GetPermissionByValue(c) == null).ToArray();
            if (invalidClaims.Any())
                return Tuple.Create(false, new[] { "The following claim types are invalid: " + string.Join(", ", invalidClaims) });


            var result = await this.roleManager.CreateAsync(role);
            if (!result.Succeeded)
                return Tuple.Create(false, result.Errors.Select(e => e.Description).ToArray());


            role = await this.roleManager.FindByNameAsync(role.Name);

            foreach (string claim in claims.Distinct())
            {
                result = await this.roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, ApplicationPermissions.GetPermissionByValue(claim)));

                if (!result.Succeeded)
                {
                    await DeleteRoleAsync(role);
                    return Tuple.Create(false, result.Errors.Select(e => e.Description).ToArray());
                }
            }

            return Tuple.Create(true, new string[] { });
        }

        public async Task<Tuple<bool, string[]>> DeleteRoleAsync(string roleName)
        {
            var role = await this.roleManager.FindByNameAsync(roleName);

            if (role != null)
                return await DeleteRoleAsync(role);

            return Tuple.Create(true, new string[] { });
        }


        public async Task<Tuple<bool, string[]>> DeleteRoleAsync(Role role)
        {
            var result = await this.roleManager.DeleteAsync(role);
            return Tuple.Create(result.Succeeded, result.Errors.Select(e => e.Description).ToArray());
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await this.userManager.FindByEmailAsync(email);
        }

        public async Task<User> RegisterUserAsync(RegisterUserDto model)
        {
            try
            {
                var user = this.mapper.Map<User>(model);
                var createUserResult = await CreateUserAsync(user, model.Password);

                return createUserResult.Item2;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<LoginResponse> LoginUserAsync(LoginRequest request)
        {
            LoginResponse loginResponse = new LoginResponse();
            try
            {
                var standardRepository = this.uow.GetStandardRepository();

                User user = (await standardRepository.QueryAsync<User>(x => x.Email == request.Email)).FirstOrDefault();
                if (user != null)
                {
                    var result = await this.signInManager.CheckPasswordSignInAsync(user, request.Password, true);
                    if (result == SignInResult.Success)
                    {
                        loginResponse.Success = true;
                        loginResponse.Message = "User has been successfully authenticated.";
                        loginResponse.Jwt = BuildJwtToken(user, true);
                    }
                }
            }
            catch (Exception ex) { }

            return loginResponse;
        }

        private string BuildJwtToken(User user, bool rememberMe)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email)
            };

            var userRoles = this.userManager.GetRolesAsync(user).Result;
            var userClaims = this.userManager.GetClaimsAsync(user).Result;

            for (int i = 0; i < userRoles.Count; i++)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRoles[i]));
            }

            for (int i = 0; i < userClaims.Count; i++)
            {
                claims.Add(new Claim(userClaims[i].ValueType, userClaims[i].Value));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            DateTime expirationDate;
            if (rememberMe)
            {
                expirationDate = DateTime.Now.AddDays(14);
            }
            else
            {
                expirationDate = DateTime.Now.AddHours(3);
            }

            var token = new JwtSecurityToken(this.configuration["Jwt:Issuer"],
              this.configuration["Jwt:Issuer"],
              claims,
              expires: expirationDate,
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<Tuple<bool, User>> CreateUserAsync(User user, string password)
        {
            try
            {
                user.EmailConfirmed = true;
                User newUser = null;
                var result = await this.userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await this.userManager.AddToRolesAsync(user, new string[] { Roles.User });
                    newUser = await this.userManager.FindByEmailAsync(user.Email);
                }

                return new Tuple<bool, User>(newUser != null, newUser);
            }
            catch (Exception)
            {
                return new Tuple<bool, User>(false, null);
            }
        }
    }
}
