using AutoMapper;
using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using FNDSystem.Core.Dto;
using FNDSystem.Core.ServicesContracts;
using System.Linq.Expressions;

namespace FNDSystem.Core.Services;
public class ObsFactService : GeneticService, IObsFactService
{
    private readonly IUnitOfWork unitOfWork;

    public ObsFactService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
    }

    #region 番号によりOBS観察事実一覧を取得する

    /// <summary>
    /// 番号によりOBS観察事実一覧を取得する
    /// </summary>
    /// <param name="num">番号</param>
    /// <returns>OBS観察事実一覧</returns>
    public IEnumerable<ObsFactResponseDto> GetObsFactListByNum(string num)
    {
        var factAttaches = unitOfWork.ObsFactRepository.GetVwObsFactAttaches(num);
        var result = factAttaches.GroupBy((x) => x.FactNum).Select(x => new ObsFactResponseDto()
        {

            Fact = x.First().Fact,
            FactTrans = x.First().FactTrans,
            Comment = x.First().Comment,
            PartTrans = x.First().PartTrans,
            ValComp = x.First().ValComp,
            PlusNeutralDelta = x.First().PlusNeutralDelta,
            FactNum = x.First().FactNum,
            OfferFields = x.First().OfferFields,
            PoCs = x.First().PoCs,
            SafetyCultures = x.First().SafetyCultures,
            LastUpdate = x.First().LastUpdate,
            LastUser = x.First().LastUser,
            DisplayOrder = x.First().DisplayOrder,
            ObsAttachs = x.Where(z =>z.SerialNum != null && z.AttachFacNum != null)
            .Where(g => g.AttachFacNum == x.First().FactNum)
            .Select(y => new ObsAttach()
            {
                SerialNum = y.SerialNum!.Value,
                NumNoRevisions = y.NumNoRevisions,
                FactNum = y.FactNum,
                RepresentPhotoFlag = y.RepresentPhotoFlag,
                FileName = y.FileName,
                Size = y.Size,
                DeleteFlag = y.DeleteFlag,
                LastUpdate = y.LastUpdate,
                LastUser = y.LastUser
            }).ToList()
        }).OrderBy(x => x.DisplayOrder);
        return result;
    }

    #endregion

    #region 番号によりOBS観察事実を取得する
    /// <summary>
    /// 番号によりOBS観察事実を取得する
    /// </summary>
    /// <param name="num">番号</param>
    /// <returns>OBS観察事実</returns>
    public ObsFact? GetObsFactByNum(string num)
    {
        Expression<Func<ObsFact, bool>> whereExp = item => item.Num == num;
        ObsFact? obsFact = unitOfWork.ObsFactRepository.Get(whereExp);

        return obsFact;
    }

    #endregion
}