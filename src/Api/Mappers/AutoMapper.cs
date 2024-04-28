using Api.Models;
using Application.Persons.Add;
using Application.Persons.ConnectPerson;
using Application.Persons.DisconnectPerson;
using Application.Persons.Models;
using Application.Persons.Search;
using Application.Persons.Update;
using AutoMapper;

namespace Api.Mappers;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CreateMap<PhoneNumberRequest, PersonPhoneNumber>();
        CreateMap<AddPersonRequest, AddPersonCommand>();
        CreateMap<UpdatePersonRequest, UpdatePersonCommand>();
        CreateMap<ConnectPersonRequest, ConnectPersonCommand>();
        CreateMap<DisconnectPersonRequest, DisconnectPersonCommand>();
        CreateMap<SearchPersonsRequest, SearchPersonsQuery>();
    }
}