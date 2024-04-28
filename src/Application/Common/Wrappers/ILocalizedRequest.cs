using Domain.Enums;

namespace Application.Common.Wrappers;

public interface ILocalizedRequest
{
    public Language Language { get; set; }
}