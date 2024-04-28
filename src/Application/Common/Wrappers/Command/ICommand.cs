using Application.Common.Models;
using Domain.Enums;
using MediatR;

namespace Application.Common.Wrappers.Command;

file interface ICommand : ILocalizedRequest;

public abstract class Command<TResponse> : IRequest<OperationResult<TResponse>>, ICommand
{
    public Language Language { get; set; }
}

public abstract class Command : IRequest<OperationResult>, ICommand
{
    public Language Language { get; set; }
}