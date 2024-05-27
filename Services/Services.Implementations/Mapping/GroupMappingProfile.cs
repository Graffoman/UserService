using AutoMapper;
using Domain.Entities;
using Services.Contracts.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations.Mapping
{
    /// <summary>
    /// Профиль автомаппера для сущности группы.
    /// </summary>
    public class GroupMappingProfile : Profile
    {
        public GroupMappingProfile()
        {
            CreateMap<Group, GroupDto>()
                .ForMember(d => d.UserGroups, map => map.MapFrom(m => m.UserGroups));

            CreateMap<CreatingGroupDto, Group>()
                .ForMember(d => d.Id, map => map.Ignore())
                .ForMember(d => d.Deleted, map => map.Ignore())
                .ForMember(d => d.UserGroups, map => map.Ignore())                
                .ForMember(d => d.Name, map => map.MapFrom(m => m.Name));

            CreateMap<UpdatingGroupDto, Group>()
                .ForMember(d => d.Id, map => map.Ignore())
                .ForMember(d => d.Deleted, map => map.Ignore())
                .ForMember(d => d.UserGroups, map => map.Ignore())
                .ForMember(d => d.Name, map => map.MapFrom(m => m.Name));
            ;
        }
    }
}
