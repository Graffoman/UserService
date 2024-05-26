using AutoMapper;
using Services.Contracts.User;
using WebApi.Models.User;

namespace WebApi.Mapping
{
    /// <summary>
    /// Профиль автомаппера для сущности пользователя.
    /// </summary>
    public class UserMappingsProfile : Profile
    {
        public UserMappingsProfile()
        {
            CreateMap<UserDto, UserModel>();
            CreateMap<CreatingUserModel, CreatingUserDto>();
            CreateMap<UpdatingUserModel, UpdatingUserDto>();
            CreateMap<UserFilterModel, UserFilterDto>();
            CreateMap<UserLoginModel, UserLoginDto>();
        }
    }
}
