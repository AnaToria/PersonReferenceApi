using Application.Common.Models;
using MediatR;

namespace Application.Common.Wrappers.Command;

public interface ICommand<TResponse> : IRequest<OperationResult<TResponse>>
{
    
}