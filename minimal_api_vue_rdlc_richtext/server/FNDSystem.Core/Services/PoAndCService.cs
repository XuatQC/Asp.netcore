using FNDSystem.Core.Domain.RepositoryContracts;
using FNDSystem.Core.Dto;
using FNDSystem.Core.ServicesContracts;

namespace FNDSystem.Core.Services;
public class PoAndCService : GeneticService, IPoAndCService
{
    private readonly IUnitOfWork unitOfWork;

    public PoAndCService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    /// <summary>
    /// PO&C一覧取得
    /// </summary>
    /// <param name="plantName">プラント名</param>
    /// <returns>達成目標（PO&C）マスタ一覧</returns>
    public IEnumerable<PoAndCResponseDto>? GetPoAndCList(string plantName)
    {
        var poAndCs = unitOfWork.PoAndCRepository.GetAll(x => x.PlantName == plantName).OrderBy(x => x.DisplayOrder);
        var poAndCResponseDtos = poAndCs.Select(x => new PoAndCResponseDto()
        {
            Code = $"{x.PoC}.{x.SerialNum}",
            Name = x.PoCName,
            NameEn = x.PoCNameEn
        });
        return poAndCResponseDtos;
    }
}