using Application.Common.Models;
using Application.Common.Wrappers.Query;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Persons.Models;

namespace Application.Persons.GetById;

public class GetByIdQueryHandler : IQueryHandler<GetByIdQuery, PersonDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IImageService _imageService;

    public GetByIdQueryHandler(IUnitOfWork unitOfWork, IImageService imageService)
    {
        _unitOfWork = unitOfWork;
        _imageService = imageService;
    }

    public async Task<OperationResult<PersonDto>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var person = await _unitOfWork.Persons.GetByIdAsync(request.Id, cancellationToken);

        var personDto = PersonDto.Create(person, _imageService, request.LanguageCode);

        return OperationResult<PersonDto>.Ok(personDto);
    }
}