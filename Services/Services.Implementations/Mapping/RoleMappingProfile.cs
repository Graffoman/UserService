using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Services.Contracts.Role;

namespace Services.Implementations.Mapping
{
    /// <summary>
    /// Профиль автомаппера для сущности роли.
    /// </summary>
    public class RoleMappingProfile : Profile
    {
        public RoleMappingProfile() {
            CreateMap<RoleDto, Role>();

            CreateMap<CreatingRoleDto, Role>()
                .ForMember(d => d.Id, map => map.Ignore())
                .ForMember(d => d.Deleted, map => map.Ignore())
                .ForMember(d => d.UserRoles, map => map.Ignore())
                .ForMember(d => d.Permissions, map => map.Ignore())
                .ForMember(d => d.Name, map => map.MapFrom(m => m.Name));

            CreateMap<UpdatingRoleDto, Role>()
                .ForMember(d => d.Id, map => map.Ignore())
                .ForMember(d => d.Deleted, map => map.Ignore())
                .ForMember(d => d.UserRoles, map => map.Ignore())
                .ForMember(d => d.Permissions, map => map.Ignore())
                .ForMember(d => d.Name, map => map.MapFrom(m => m.Name));
            ;
        }
    }
}
