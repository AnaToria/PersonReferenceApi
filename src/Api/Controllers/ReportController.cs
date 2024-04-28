using Application.Common.Models;
using Application.Reports.GetRelationships;
using Application.Reports.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class ReportController : BaseController
{
    public ReportController(IServiceProvider serviceProvider) 
        : base(serviceProvider)
    {
    }
    
    [HttpGet("relationships")]
    public Task<OperationResult<IEnumerable<PersonReportListItemDto>>> GetRelationships(CancellationToken cancellationToken)
    {
        return SendQueryAsync(new GetRelationshipsReportQuery(), cancellationToken);
    }
}