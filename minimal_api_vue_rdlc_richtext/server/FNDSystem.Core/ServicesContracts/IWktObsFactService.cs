using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Dto;

namespace FNDSystem.Core.ServicesContracts;

public interface IWktObsFactService
{
    bool DeleteById(int id);
    bool RemoveAll();
    WktObsFact Create(WktObsFactDto wktObsFactDto);
}
