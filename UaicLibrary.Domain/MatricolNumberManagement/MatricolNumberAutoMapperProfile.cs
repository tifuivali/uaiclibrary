using AutoMapper;
using UaicLibrary.DataBase.Models;

namespace UaicLibrary.Domain.MatricolNumberManagement
{
    public class MatricolNumberAutoMapperProfile : Profile
    {
        public MatricolNumberAutoMapperProfile()
        {
            CreateMap<MatricolNumberDto, MatricolNumber>().ReverseMap();
        }
    }
}
