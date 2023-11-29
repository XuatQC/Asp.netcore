using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Dto;

namespace FNDSystem.Core.ServicesContracts;

public interface IObsFactService
{
    IEnumerable<ObsFactResponseDto> GetObsFactListByNum(string num);
    ObsFact? GetObsFactByNum(string num);
}