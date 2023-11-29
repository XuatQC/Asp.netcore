using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Dto;

namespace FNDSystem.Core.ServicesContracts;

public interface IWktTransHistoryService
{
    bool RemoveAll();
    IEnumerable<WktTransHistory>? GetMany(FindWktTransHistoryRequest findWktTranslateDto);
    bool ProcessDataHandler(WktTransHistoryRequest wktTransHistoryRequest);
}
