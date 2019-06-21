using DataTransferObjects.Entities.Identity;
using DataTransferObjects.HttpBodies.Request;
using DataTransferObjects.HttpBodies.Response;
using Entities.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public interface IIdentityService
    {
        Task<LoginResponse> LoginUserAsync(LoginRequest request);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> RegisterUserAsync(RegisterUserDto model);
        Task<Tuple<bool, string[]>> CreateRoleAsync(Role role, IEnumerable<string> claims);
        Task<Tuple<bool, string[]>> DeleteRoleAsync(Role role);
        Task<Tuple<bool, string[]>> DeleteRoleAsync(string roleName);
        Task<Tuple<bool, User>> CreateUserAsync(User user, string password);
    }
}
