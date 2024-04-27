using Application.Common.Wrappers.Query;
using Microsoft.AspNetCore.Http;

namespace Application.Multimedia.Get;

public class GetMultimediaQuery : Query<byte[]>
{
    public string FileName { get; set; }
}