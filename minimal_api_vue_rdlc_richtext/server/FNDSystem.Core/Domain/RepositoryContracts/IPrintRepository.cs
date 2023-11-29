using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Dto;

namespace FNDSystem.Core.Domain.RepositoryContracts;
public interface IPrintRepository : IGenericRepository<PrintDataResponeseDto>
{
    IEnumerable<PrintDataResponeseDto>? SetDataListToPrint(FindPrintRequestDto findDto);
    List<VwTranslateControl>? PreOutputWord(FindTranslateRequestDto findTranslateRequestDto);
    bool ProcessingOutputWord(string inLang, List<VwTranslateControl> preTranslateList, ObsPathsDto obsPathsDto);
}
