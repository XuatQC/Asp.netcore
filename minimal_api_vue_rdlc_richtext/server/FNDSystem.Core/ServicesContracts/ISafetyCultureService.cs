using FNDSystem.Core.Domain;
using FNDSystem.Core.Dto;

namespace FNDSystem.Core.ServicesContracts;

public interface ISafetyCultureService
{
    IEnumerable<SafetyCultureResponseDto>? GetSafetyCultureList(string plantName);
}