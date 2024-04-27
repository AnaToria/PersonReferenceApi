using Application.Common.Models;
using MediatR;

namespace Application.Common.Wrappers.Query;

file interface IQuery : ILocalizedRequest;

public abstract class Query<TResponse> : IRequest<OperationResult<TResponse>>, IQuery
{
    public string LanguageCode { get; set; }
}