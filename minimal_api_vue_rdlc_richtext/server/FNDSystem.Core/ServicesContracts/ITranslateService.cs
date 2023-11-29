using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Dto;

namespace FNDSystem.Core.ServicesContracts;

public interface ITranslateService
{
    VwTranslateControl? GetTranslate(FindTranslateRequestDto findTranslateDto);
    IEnumerable<VwTranslateControl>? GetMany(FindTranslateRequestDto findTranslateDto);
    List<OfferNumResponseDto>? GetOfferNumList(FindTranslateRequestDto findTranslateDto);
    bool IsTransCompConfirm(string num);
    bool CreateTransData(FindTranslateRequestDto findTranslateDto, string pTableName, string gPlantName);
    bool LoadTranslateData(string plantName);
}