using System.Net.Mime;
using Application.Common.Models;
using Application.Multimedia.Add;
using Application.Multimedia.Get;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class MultimediaController : BaseController
{
    public MultimediaController(IServiceProvider serviceProvider) 
        : base(serviceProvider)
    {
    }

    [HttpGet("get")]
    public async Task<IActionResult> Get(string fileName, CancellationToken cancellationToken)
    {
        var query = new GetMultimediaQuery
        {
            FileName = fileName
        };
        var result = await SendQueryAsync(query, cancellationToken);

        return new FileContentResult(result.Data, MediaTypeNames.Image.Jpeg);
    }
    
    [HttpPost("upload")]
    public Task<OperationResult<string>> UploadImage([FromForm] IFormFile file, CancellationToken cancellationToken)
    {
        var command = new AddMultimediaCommand
        {
            File = file
        };
        return SendCommandAsync(command, cancellationToken);
    }
}