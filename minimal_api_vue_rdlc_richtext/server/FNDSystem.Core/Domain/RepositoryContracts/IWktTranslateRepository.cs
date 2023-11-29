using FNDSystem.Core.Domain.Entities;

namespace FNDSystem.Core.Domain.RepositoryContracts;
public interface IWktTranslateRepository : IGenericRepository<WktTranslate>
{

    public void DeleteAll();
}