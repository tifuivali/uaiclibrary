using AutoMapper;
using UaicLibrary.DataBase.Models;

namespace UaicLibrary.Domain.UserManagement
{
    public class UserAutoMapperProfile: Profile
    {
        public UserAutoMapperProfile()
        {
            CreateMap<StudentDto, User>().ReverseMap();
            CreateMap<StudentDto, UserInfo>()
                .ForMember(x => x.FullName, opt => opt.MapFrom(x => $"{x.FirstName} {x.LastName}"));
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
