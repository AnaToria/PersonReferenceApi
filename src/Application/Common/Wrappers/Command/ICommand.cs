using Application.Common.Models;
using MediatR;

namespace Application.Common.Wrappers.Command;

file interface ICommand : ILocalizedRequest;

public abstract class Command<TResponse> : IRequest<OperationResult<TResponse>>, ICommand
{
    public string LanguageCode { get; set; }
}