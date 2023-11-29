using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Dto;

namespace FNDSystem.Core.Domain.RepositoryContracts;
public interface ITranslateRepository : IGenericRepository<Translate>
{
    List<OfferNumResponseDto>? GetOfferNumList(FindTranslateRequestDto findTranslateDto);
    bool CreateTranslationData(FindTranslateRequestDto findTranslateDto, string pTableName, string gPlantName);

}