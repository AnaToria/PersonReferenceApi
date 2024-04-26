using Application.Common.Models;
using MediatR;

namespace Application.Common.Wrappers.Query;

public interface IQuery<TResponse> : IRequest<OperationResult<TResponse>>
{
    
}