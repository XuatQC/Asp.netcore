using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Dto;

namespace FNDSystem.Core.ServicesContracts;

public interface IWktObsConclusionService
{
    bool DeleteById(int id);
    bool RemoveAll();
    WktObsConclusion Create(WktObsConclusionDto wktObsConclusionDto);
}
