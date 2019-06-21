using DataTransferObjects.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();

        Task<UserDto> GetUserAsync(Guid id);
    }
}
