using AutoMapper;
using DataTransferObjects.Entities.Identity;
using Entities.Identity;

namespace Core.MappingProfiles
{
    public class IdentityMappingProfile : Profile
    {
        public IdentityMappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
                .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
                .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
                .ReverseMap();

            CreateMap<RegisterUserDto, User>()
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
                .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.Email))
                .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
                .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
                .ForAllOtherMembers(x => x.Ignore());
        }
    }
}
