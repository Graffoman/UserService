using AutoMapper;
using Domain.Entities;
using Services.Contracts.Permission;

namespace Services.Implementations.Mapping
{
    /// <summary>
    /// Профиль автомаппера для сущности права доступа.
    /// </summary>
    public class PermissionMappingsProfile : Profile
    {
        public PermissionMappingsProfile()
        {
            CreateMap<Permission, PermissionDto>();

            CreateMap<CreatingPermissionDto, Permission>()
                .ForMember(d => d.Id, map => map.Ignore())
                .ForMember(d => d.Deleted, map => map.Ignore())
                .ForMember(d => d.Role, map => map.Ignore())
                .ForMember(d => d.RoleId, map => map.Ignore())
                .ForMember(d => d.Name, map => map.MapFrom(m=>m.Name));
            
            CreateMap<UpdatingPermissionDto, Permission>()
                .ForMember(d => d.Id, map => map.Ignore())
                .ForMember(d => d.Deleted, map => map.Ignore())
                .ForMember(d => d.Role, map => map.Ignore())
                .ForMember(d => d.RoleId, map => map.Ignore())
                .ForMember(d => d.Name, map => map.MapFrom(m => m.Name));
            ;
        }
    }
}
