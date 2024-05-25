using AutoMapper;
using Services.Contracts.Permission;
using WebApi.Models.Permission;

namespace WebApi.Mapping
{
    /// <summary>
    /// Профиль автомаппера для сущности права доступа.
    /// </summary
    public class PermissionMappingsProfile : Profile
    {        
        public PermissionMappingsProfile()
        {
            CreateMap<PermissionDto, PermissionModel>();
            CreateMap<CreatingPermissionDto, CreatingPermissionModel>();
            CreateMap<UpdatingPermissionDto, UpdatingPermissionModel>();
        }
        
    }
}
