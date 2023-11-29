using FNDSystem.Core.Domain.Entities;

namespace FNDSystem.Core.Domain.RepositoryContracts;
public interface IWkWordListRepository : IGenericRepository<WkWordList>
{
    IEnumerable<WkWordList>? SortDataList();
}