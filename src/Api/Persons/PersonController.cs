using Application.Common.Models;
using Application.Persons.AddPerson;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Persons;

[Route("api/persons")]
[ApiController]
public class PersonController : Controller
{
    private readonly IMediator _mediator;

    public PersonController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [Route("add")]
    [HttpPost]
    public async Task<OperationResult<int>> AddPerson([FromBody] AddPersonRequest request)
    {
        return await _mediator.Send(new AddPersonCommand(request));
    }
}