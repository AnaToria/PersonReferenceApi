using Domain.Entities;
using MediatR;

namespace Application.Cities;

public record GetCitiesQuery : IRequest<IEnumerable<City>>
{
}