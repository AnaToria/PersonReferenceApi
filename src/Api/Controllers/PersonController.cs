using Api.Models;
using Application.Common.Models;
using Application.Persons.Add;
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
}