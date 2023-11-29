using FNDSystem.Core.Domain;
using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Dto;

namespace FNDSystem.Core.ServicesContracts;

public interface IFieldService
{
    IEnumerable<Field> FindMany(FindFieldRequestDto? findFieldDto);
    IEnumerable<FieldResponseDto>? GetFieldList(FindFieldRequestDto findFieldDto);
    IEnumerable<Field>? Get_11Records_Field(FindFieldRequestDto findFieldDto);
}

