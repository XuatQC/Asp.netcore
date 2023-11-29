using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using FNDSystem.Core.Dto;
using FNDSystem.Core.Extensions;
using FNDSystem.Core.ServicesContracts;
using System.Linq.Expressions;

namespace FNDSystem.Core.Services;
public class ItemMasterService : GeneticService, IItemMasterService
{
    private readonly IUnitOfWork unitOfWork;

    public ItemMasterService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    /// <summary>
    /// 検索
    /// </summary>
    /// <param name="findItemMasterDto">条件検索</param>
    /// <returns>アイテムマスタ一覧</returns>
    public IEnumerable<ItemMaster> FindMany(FindItemMasterRequestDto? findItemMasterDto)
    {
        Expression<Func<ItemMaster, bool>>? whereExpression = null;

        if (!string.IsNullOrEmpty(findItemMasterDto?.Item))
        {
            whereExpression = itemMaster => itemMaster.Item == findItemMasterDto.Item;
        }
        if (!string.IsNullOrEmpty(findItemMasterDto?.Code))
        {
            Expression<Func<ItemMaster, bool>> byCode = itemMaster => itemMaster.Code == findItemMasterDto.Code;
            if (whereExpression != null)
            {
                whereExpression = whereExpression.AndEx<ItemMaster>(byCode);
            }
            else
            {
                whereExpression = itemMaster => itemMaster.Code == findItemMasterDto.Code;
            }
        }

        var itemMasters = unitOfWork.ItemMasterRepository.GetAll(whereExpression, 0, null);

        return itemMasters;
    }
}