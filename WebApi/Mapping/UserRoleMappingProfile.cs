using AutoMapper;
using Services.Contracts.UserRole;
using WebApi.Models.UserRole;

namespace WebApi.Mapping
{
    /// <summary>
    /// Профиль автомаппера для сущности роли пользователя
    /// </summary
    public class UserRoleMappingsProfile : Profile
    {
        public UserRoleMappingsProfile()
        {
            CreateMap<UserRoleDto, UserRoleModel>();
            CreateMap<CreatingUserRoleModel, CreatingUserRoleDto>();
        }

    }
}