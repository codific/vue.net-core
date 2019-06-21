using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DataTransferObjects.Entities.Identity;
using Entities.Identity;
using GSK.DAL;

namespace Services
{
    public class UserService : AbstractService, IUserService
    {
        public UserService(IUnitOfWork uow, IMapper mapper) : base(uow, mapper)
        {

        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            try
            {
                var standardRepository = this.uow.GetStandardRepository();
                var users = await standardRepository.GetAllAsync<User>();
                var usersDto = this.mapper.Map<IEnumerable<UserDto>>(users);

                return usersDto;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<UserDto> GetUserAsync(Guid id)
        {
            try
            {
                var standardRepository = this.uow.GetStandardRepository();
                var user = await standardRepository.GetAsync<User>(id);
                var userDto = this.mapper.Map<UserDto>(user);

                return userDto;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
