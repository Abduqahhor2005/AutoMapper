using AutoMapper;
using MappingByAutoMapper.Entities;

namespace MappingByAutoMapper;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<Person, ReadPerson>();
        CreateMap<CreatePerson,Person>().ReverseMap();
        CreateMap<UpdatePerson,Person>();
    }
}