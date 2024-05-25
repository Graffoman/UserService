using AutoMapper;
using Domain.Entities;
using Services.Contracts.UserRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations.Mapping
{
    /// <summary>
    /// Профиль автомаппера для сущности рои пользователя.
    /// </summary>
    public class UserRoleMappingProfile : Profile
    {
        public UserRoleMappingProfile()
        {
            CreateMap<UserRoleDto, UserRole>();

            CreateMap<CreatingUserRoleDto, UserRoleDto>()
                .ForMember(d => d.Id, map => map.Ignore())
                .ForMember(d => d.User, map => map.Ignore())               
                .ForMember(d => d.Role, map => map.Ignore());      

        }
    }
}
