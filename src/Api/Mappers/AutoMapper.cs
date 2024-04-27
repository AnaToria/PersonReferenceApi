using Api.Models;
using Application.Persons.AddPerson;
using Application.Persons.Update;
using Application.Persons.UploadImage;
using AutoMapper;

namespace Api.Mappers;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CreateMap<AddPersonRequest, AddPersonCommand>();
        CreateMap<UploadImageRequest, UploadImageCommand>();
        CreateMap<UpdatePersonRequest, UpdatePersonCommand>();
    }
}