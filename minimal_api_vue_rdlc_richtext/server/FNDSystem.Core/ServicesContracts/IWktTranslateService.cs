using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Dto;

namespace FNDSystem.Core.ServicesContracts;

public interface IWktTranslateService
{
    WktTranslate? GetWktTranslate(FindWktTranslateRequestDto findWktTranslateDto);
    IEnumerable<WktTranslate>? GetMany(FindWktTranslateRequestDto findWktTranslateDto);
    void DeleteAll();
}