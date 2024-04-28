using Application.Common.Models;
using Application.Reports.Get;
using Application.Reports.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class ReportController : BaseController
{
    private readonly IMapper _mapper;

    public ReportController(IServiceProvider serviceProvider, IMapper mapper) 
        : base(serviceProvider)
    {
        _mapper = mapper;
    }
    
    [HttpGet]
    public Task<OperationResult<IEnumerable<PersonReportListItemDto>>> Get(CancellationToken cancellationToken)
    {
        return SendQueryAsync(new GetReportQuery(), cancellationToken);
    }
}