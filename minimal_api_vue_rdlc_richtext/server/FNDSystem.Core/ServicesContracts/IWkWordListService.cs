using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Dto;

namespace FNDSystem.Core.ServicesContracts;

public interface IWkWordListService
{
    bool RemoveAll();
    IEnumerable<PrintDataResponeseDto>? SetDataListToPrint(FindPrintRequestDto printDto);
    IEnumerable<WkWordList>? SortDataList();
}
