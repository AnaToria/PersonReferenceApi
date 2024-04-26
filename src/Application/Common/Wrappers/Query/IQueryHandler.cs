using Application.Common.Models;
using MediatR;

namespace Application.Common.Wrappers.Query;

public interface IQueryHandler<in TRequest, TResponse> :
    IRequestHandler<TRequest, OperationResult<TResponse>>
    where TRequest : IQuery<TResponse>
{
    
}