using Microsoft.EntityFrameworkCore;

namespace FNDSystem.Core.Dto
{
    [Keyless]
    public class ReviewMemberResponseDto
    {
        public string? Initial { get; set; }
        public string? Name { get; set; }
    }
}
