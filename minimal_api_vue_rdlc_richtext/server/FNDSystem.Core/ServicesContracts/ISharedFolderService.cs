using FNDSystem.Core.Domain.Entities;

namespace FNDSystem.Core.ServicesContracts;

public interface ISharedFolderService
{
    IEnumerable<SharedFolder>? GetSharedFolderMenu(string plantName);
    IEnumerable<SharedFolder>? GetSharedFolderList(string plantName);
}