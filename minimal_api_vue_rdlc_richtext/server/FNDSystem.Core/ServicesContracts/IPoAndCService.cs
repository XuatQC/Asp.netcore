using FNDSystem.Core.Domain;
using FNDSystem.Core.Dto;

namespace FNDSystem.Core.ServicesContracts;

public interface IPoAndCService
{
    IEnumerable<PoAndCResponseDto>? GetPoAndCList(string plantName);
}