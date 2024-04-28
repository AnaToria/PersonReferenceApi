using Api.Models;
using Application.Persons.Add;
using Application.Persons.ConnectPerson;
using Application.Persons.DisconnectPerson;
using Application.Persons.Update;
using AutoMapper;

namespace Api.Mappers;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CreateMap<AddPersonRequest, AddPersonCommand>();
        CreateMap<UpdatePersonRequest, UpdatePersonCommand>();
        CreateMap<ConnectPersonRequest, ConnectPersonCommand>();
        CreateMap<DisconnectPersonRequest, DisconnectPersonCommand>();
    }
}