using Application.Common.Models;
using Application.Persons.AddPerson;
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
    public Task<OperationResult<int>> AddPerson([FromBody] AddPersonRequest request)
    {
        return _mediator.Send(_mapper.Map<AddPersonCommand>(request));
    }
}