using FNDSystem.Core.Domain.RepositoryContracts;
using FNDSystem.Core.Dto;
using FNDSystem.Core.ServicesContracts;

namespace FNDSystem.Core.Services;
public class SafetyCultureService : GeneticService, ISafetyCultureService
{
    private readonly IUnitOfWork unitOfWork;

    public SafetyCultureService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    /// <summary>
    /// 安全文化一覧取得
    /// </summary>
    /// <param name="plantName">検索条件</param>
    /// <returns>安全文化一覧</returns>
    public IEnumerable<SafetyCultureResponseDto>? GetSafetyCultureList(string plantName)
    {
        var safetyCultures = unitOfWork.SafetyCultureRepository.GetAll(x => x.PlantName == plantName).OrderBy(x => x.DisplayOrder);
        var safetyCultureResponseDto = safetyCultures.Select(x => new SafetyCultureResponseDto()
        {
            Code = $"{x.Code}.{x.SerialNum}",
            Name = x.Name,
            NameEn = x.NameEn
        });
        return safetyCultureResponseDto;
    }
}