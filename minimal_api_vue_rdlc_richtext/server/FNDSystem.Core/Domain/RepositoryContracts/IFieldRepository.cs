using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Dto;

namespace FNDSystem.Core.Domain.RepositoryContracts;
public interface IFieldRepository : IGenericRepository<Field>
{
    public IEnumerable<FieldResponseDto>? GetFieldList(FindFieldRequestDto findFieldDto);
    public IEnumerable<Field>? Get_11Records_Field(FindFieldRequestDto findFieldDto);
}