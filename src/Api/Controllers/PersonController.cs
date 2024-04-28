using Api.Models;
using Application.Common.Models;
using Application.Persons.Add;
using Application.Persons.ConnectPerson;
using Application.Persons.Delete;
using Application.Persons.DisconnectPerson;
using Application.Persons.GetById;
using Application.Persons.Models;
using Application.Persons.Update;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class PersonController : BaseController
{
    private readonly IMapper _mapper;

    public PersonController(IServiceProvider serviceProvider, IMapper mapper) 
        : base(serviceProvider)
    {
        _mapper = mapper;
    }
    
    [HttpGet("get/{id}")]
    public Task<OperationResult<PersonDto>> Get([FromRoute] int id, CancellationToken cancellationToken)
    {
        var query = new GetByIdQuery { Id = id };
        return SendQueryAsync(query, cancellationToken);
    }
    
    [HttpPost("add")]
    public Task<OperationResult<int>> Add([FromBody] AddPersonRequest request, CancellationToken cancellationToken)
    {
        return SendCommandAsync(_mapper.Map<AddPersonCommand>(request), cancellationToken);
    }
    
    [HttpPut("update/{id}")]
    public Task<OperationResult<int>> Update([FromBody] UpdatePersonRequest request, [FromRoute] int id, CancellationToken cancellationToken)
    {
        var mappedRequest = _mapper.Map<UpdatePersonCommand>(request);
        mappedRequest.Id = id;
        
        return SendCommandAsync(mappedRequest, cancellationToken);
    }
    
    [HttpDelete("delete/{id}")]
    public Task<OperationResult<bool>> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        var command = new DeletePersonCommand
        {
            Id = id
        };
        return SendCommandAsync(command, cancellationToken);
    }

    [HttpPost("connect")]
    public Task<OperationResult> Connect([FromBody] ConnectPersonRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<ConnectPersonCommand>(request);

        return SendCommandAsync(command, cancellationToken);
    }
    
    [HttpPost("disconnect")]
    public Task<OperationResult> Disconnect([FromBody] DisconnectPersonRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<DisconnectPersonCommand>(request);

        return SendCommandAsync(command, cancellationToken);
    }
}