using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Cities;

internal class GetCitiesQueryHandler : IRequestHandler<GetCitiesQuery, IEnumerable<City>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCitiesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<City>> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
    {
        var cities = await _unitOfWork.Cities.GetAllAsync(cancellationToken);

        return cities;
    }
}
