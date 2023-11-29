using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Dto;

namespace FNDSystem.Core.ServicesContracts;

public interface IPrintService
{
    List<VwTranslateControl>? PreOutputWord(FindTranslateRequestDto findTranslateRequestDto);
    bool ProcessingOutputWord(string inLang, List<VwTranslateControl> preTranslateList, ObsPathsDto obsPathsDto);
}

