using AutoMapper;
using Services.Contracts.UserGroup;
using WebApi.Models.UserGroup;

namespace WebApi.Mapping
{
    /// <summary>
    /// Профиль автомаппера для сущности группы пользователя
    /// </summary
    public class UserGroupMappingsProfile : Profile
    {
        public UserGroupMappingsProfile()
        {
            CreateMap<UserGroupDto, UserGroupModel>();
            CreateMap<CreatingUserGroupModel, CreatingUserGroupDto>();           
        }

    }
}