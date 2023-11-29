using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Dto;

namespace FNDSystem.Core.ServicesContracts;

public interface IItemMasterService
{
    IEnumerable<ItemMaster> FindMany(FindItemMasterRequestDto? findItemMasterDto);
}
