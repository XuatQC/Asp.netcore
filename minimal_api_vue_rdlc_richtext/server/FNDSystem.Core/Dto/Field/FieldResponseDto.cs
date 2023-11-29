using Microsoft.EntityFrameworkCore;

namespace FNDSystem.Core.Dto
{
    [Keyless]
    public class FieldResponseDto
    {
        public string? Fields { get; set; }
        public string? FieldsName { get; set; }
        public string? FieldsNameEn { get; set; }
        public int? DisplayOrder { get; set; }
    }
}
