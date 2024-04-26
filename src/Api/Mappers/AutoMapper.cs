using Application.Persons.AddPerson;
using AutoMapper;

namespace Api.Mappers;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CreateMap<AddPersonRequest, AddPersonCommand>();
    }
}