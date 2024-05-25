using AutoMapper;
using Services.Contracts.Role;
using WebApi.Models.Role;

namespace WebApi.Mapping
{
    /// <summary>
    /// Профиль автомаппера для сущности роли.
    /// </summary>
    public class RoleMappingsProfile : Profile
    {
        public RoleMappingsProfile()
        {
            CreateMap<RoleDto, RoleModel>();
            CreateMap<CreatingRoleModel, CreatingRoleDto>();
            CreateMap<UpdatingRoleModel, UpdatingRoleDto>();
        }
    }
}
