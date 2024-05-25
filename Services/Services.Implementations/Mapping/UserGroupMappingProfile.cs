using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Services.Contracts.UserGroup;

namespace Services.Implementations.Mapping
{
    /// <summary>
    /// Профиль автомаппера для сущности группы пользователей.
    /// </summary>
    public class UserGroupMappingProfile : Profile
    {
        public UserGroupMappingProfile()
        {
            CreateMap<UserGroupDto, UserGroup>();

            CreateMap<CreatingUserGroupDto, UserGroup>()
                .ForMember(d => d.Id, map => map.Ignore())
                .ForMember(d => d.User, map => map.Ignore())
                .ForMember(d => d.Group, map => map.Ignore());

        }
    }
}
