using FNDSystem.Core.Domain.Entities;

namespace FNDSystem.Core.Domain.RepositoryContracts;
public interface IObsFactRepository : IGenericRepository<ObsFact>
{
    IEnumerable<VwObsFactAttach> GetVwObsFactAttaches(string num);
}