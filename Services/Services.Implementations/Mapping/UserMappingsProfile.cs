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
                .ForMember(d => d.Groups, map => map.Ignore())
                .ForMember(d => d.Roles, map => map.Ignore());

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

            CreateMap<UserLoginDto, User>()
                .ForMember(d => d.Id, map => map.Ignore())
                .ForMember(d => d.Name, map => map.Ignore())
                .ForMember(d => d.LastName, map => map.Ignore())
                .ForMember(d => d.MiddleName, map => map.Ignore())
                .ForMember(d => d.Department, map => map.Ignore())
                .ForMember(d => d.BirthdayDate, map => map.Ignore())
                .ForMember(d => d.Email, map => map.Ignore())
                .ForMember(d => d.Deleted, map => map.Ignore())
                .ForMember(d => d.PasswordHash, map => map.Ignore())
                .ForMember(d => d.UserGroups, map => map.Ignore())
                .ForMember(d => d.UserRoles, map => map.Ignore());
        }
    }
}
