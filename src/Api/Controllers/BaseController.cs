using Api.Common;
using Application.Common.Models;
using Application.Common.Wrappers.Command;
using Application.Common.Wrappers.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController  : Controller
{
    private IMediator Mediator { get; }
    private IHttpContextAccessor HttpContextAccessor { get; }
    protected BaseController(IServiceProvider serviceProvider)
    {
        Mediator = serviceProvider.GetRequiredService<IMediator>();
        HttpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
    }

    protected Task<OperationResult<TResponse>> SendQueryAsync<TResponse>(Query<TResponse> query, CancellationToken cancellationToken)
    {
        query.LanguageCode = HttpContextAccessor.HttpContext.Request.Headers[Constants.LanguageHeaderName];
        return Mediator.Send(query, cancellationToken);
    }
    
    protected Task<OperationResult<TResponse>> SendCommandAsync<TResponse>(Command<TResponse> command, CancellationToken cancellationToken)
    {
        command.LanguageCode = HttpContextAccessor.HttpContext.Request.Headers[Constants.LanguageHeaderName];
        return Mediator.Send(command, cancellationToken);
    }
    
    protected Task<OperationResult> SendCommandAsync(Command command, CancellationToken cancellationToken)
    {
        command.LanguageCode = HttpContextAccessor.HttpContext.Request.Headers[Constants.LanguageHeaderName];
        return Mediator.Send(command, cancellationToken);
    }
}