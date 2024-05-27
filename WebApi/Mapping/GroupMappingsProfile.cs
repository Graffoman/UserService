using AutoMapper;
using Services.Contracts.Group;
using WebApi.Models.Group;

namespace WebApi.Mapping
{ 
    /// <summary>
    /// Профиль автомаппера для сущности группы.
    /// </summary>
    public class GroupMappingsProfile : Profile
    {
        public GroupMappingsProfile()
        {
            CreateMap<GroupDto, GroupModel>();
            //CreateMap<CreatingGroupDto, CreatingGroupModel>();
            //CreateMap<UpdatingGroupDto, UpdatingGroupModel>();
            CreateMap<CreatingGroupModel, CreatingGroupDto>();
            CreateMap<UpdatingGroupModel, UpdatingGroupDto>();
        }
    }
}
