using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Dto;

namespace FNDSystem.Core.ServicesContracts;

public interface IObsConclusionService
{
    IEnumerable<ObsConclusion> GetObsConclusionListByNum(string num);
    ObsConclusion? GetObsConclusionByNum(string num);
}