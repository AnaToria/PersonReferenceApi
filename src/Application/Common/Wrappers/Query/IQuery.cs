using Application.Common.Models;
using Domain.Enums;
using MediatR;

namespace Application.Common.Wrappers.Query;

file interface IQuery : ILocalizedRequest;

public abstract class Query<TResponse> : IRequest<OperationResult<TResponse>>, IQuery
{
    public Language Language { get; set; }
}