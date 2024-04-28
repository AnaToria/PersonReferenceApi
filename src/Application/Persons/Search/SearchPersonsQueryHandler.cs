using Application.Common.Models;
using Application.Common.Wrappers.Query;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Persons.Models;
using Domain.Enums;

namespace Application.Persons.Search;

public class SearchPersonsQueryHandler : IQueryHandler<SearchPersonsQuery, IEnumerable<PersonListItemDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IImageService _imageService;

    public SearchPersonsQueryHandler(IUnitOfWork unitOfWork, IImageService imageService)
    {
        _unitOfWork = unitOfWork;
        _imageService = imageService;
    }

    public async Task<OperationResult<IEnumerable<PersonListItemDto>>> Handle(SearchPersonsQuery request, CancellationToken cancellationToken)
    {
        var persons = await _unitOfWork.Persons.SearchAsync(request.Name,
            request.Surname,
            request.Pin,
            request.Gender,
            request.BirthDateFrom,
            request.BirthDateTo,
            request.CityId,
            request.PageNumber,
            request.PageSize,
            cancellationToken);
        
        var result = persons.Select(person =>
        {
            var city = new CityDto
            {
                Id = person.City.Id,
                Name = request.Language == Language.Ka ? person.City.Name : person.City.NameEn
            };

            return new PersonListItemDto
            {
                Id = person.Id,
                BirthDate = person.BirthDate,
                City = city,
                Gender = person.Gender,
                Image = _imageService.GetImageUrl(person.Image),
                Name = person.Name,
                Surname = person.Surname,
                Pin = person.Pin
            };
        });

        return OperationResult<IEnumerable<PersonListItemDto>>.Ok(result);
    }
}