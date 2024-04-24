using Application.Cities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Cities;

[Route("api/cities")]
[ApiController]
public class CitiesController : Controller
{
    private readonly IMediator _mediator;

    public CitiesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Route("")]
    [HttpGet]
    public async Task<IActionResult> GetCities()
    {
        var result = await _mediator.Send(new GetCitiesQuery());
        return Ok(result);
    }
}