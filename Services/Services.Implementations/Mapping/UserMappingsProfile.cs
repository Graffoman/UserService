using AutoMapper;
using Domain.Entities;
using Services.Contracts.User;

namespace Services.Implementations.Mapping
{
    /// <summary>
    /// Профиль автомаппера для сущности пользователя.
    /// </summary>
    public class UserMappingsProfile : Profile
    {
        public UserMappingsProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(d => d.UserGroups, map => map.MapFrom(m => m.UserGroups))
                .ForMember(d => d.UserRoles, map => map.MapFrom(m => m.UserRoles));

            CreateMap<CreatingUserDto, User>()
                .ForMember(d => d.Id, map => map.Ignore())
                .ForMember(d => d.Deleted, map => map.Ignore())
                .ForMember(d => d.PasswordHash, map => map.Ignore())
                .ForMember(d => d.UserGroups, map => map.Ignore())               
                .ForMember(d => d.UserRoles, map => map.Ignore()); 
      
            CreateMap<UpdatingUserDto, User>()
                .ForMember(d => d.Id, map => map.Ignore())
                .ForMember(d => d.Deleted, map => map.Ignore())
                .ForMember(d => d.PasswordHash, map => map.Ignore())
                .ForMember(d => d.UserGroups, map => map.Ignore())
                .ForMember(d => d.UserRoles, map => map.Ignore());
        }
    }
}
