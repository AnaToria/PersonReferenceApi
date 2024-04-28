using Application.Common.Wrappers.Query;

namespace Application.Multimedia.Get;

public class GetMultimediaQuery : Query<byte[]>
{
    public string FileName { get; set; }
}