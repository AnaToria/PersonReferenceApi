using Application.Common.Models;
using MediatR;

namespace Application.Common.Wrappers.Command;

public interface ICommandHandler<in TRequest, TResponse> :
    IRequestHandler<TRequest, OperationResult<TResponse>>
    where TRequest : Command<TResponse>;