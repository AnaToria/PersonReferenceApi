using Api.Models;
using Application.Common.Models;
using Application.Persons.AddPerson;
using Application.Persons.Update;
using Application.Persons.UploadImage;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Persons;

[Route("api/persons")]
[ApiController]
public class PersonController : Controller
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;


    public PersonController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    [Route("add")]
    [HttpPost]
    public Task<OperationResult<int>> Add([FromBody] AddPersonRequest request)
    {
        return _mediator.Send(_mapper.Map<AddPersonCommand>(request));
    }
    
    [Route("image/upload/{id}")]
    [HttpPost]
    public Task<OperationResult<string?>> UploadImage(IFormFile image, [FromRoute] int id)
    {
        return _mediator.Send(_mapper.Map<UploadImageCommand>(UploadImageRequest.Create(image, id)));
    }
    
    [Route("update/{id}")]
    [HttpPut]
    public Task<OperationResult<int>> Update([FromBody] UpdatePersonRequest request, [FromRoute] int id)
    {
        var mappedRequest = _mapper.Map<UpdatePersonCommand>(request);
        mappedRequest.Id = id;
        
        return _mediator.Send(mappedRequest);
    }
}