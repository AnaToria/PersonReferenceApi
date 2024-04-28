using Application.Common.Models;
using Application.Common.Wrappers.Query;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Persons.Models;
using Application.Reports.Models;
using Domain.Enums;

namespace Application.Reports.Get;

public class GetReportQueryHandler : IQueryHandler<GetReportQuery, IEnumerable<PersonReportListItemDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IImageService _imageService;

    public GetReportQueryHandler(IUnitOfWork unitOfWork, IImageService imageService)
    {
        _unitOfWork = unitOfWork;
        _imageService = imageService;
    }

    public async Task<OperationResult<IEnumerable<PersonReportListItemDto>>> Handle(GetReportQuery request, CancellationToken cancellationToken)
    {
        var persons = await _unitOfWork.Persons.GetAllWithRelationships(cancellationToken);
        
        var result = persons.Select(person => new PersonReportListItemDto
        {
            Id = person.Id,
            Name = person.Name,
            Surname = person.Surname,
            Gender = person.Gender,
            Pin = person.Pin,
            BirthDate = person.BirthDate,
            Image = _imageService.GetImageUrl(person.Image),
            PersonRelationshipsByTypes = person.Relationships
                .GroupBy(r => r.RelationshipType)
                .Select(g => new PersonRelationshipsByTypeDto
                {
                    RelationshipType = g.Key,
                    Count = g.Count()
                })
                .ToList()
        });
        
        return OperationResult<IEnumerable<PersonReportListItemDto>>.Ok(result);
    }
}