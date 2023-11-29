using Microsoft.EntityFrameworkCore;

namespace FNDSystem.Core.Dto
{
    [Keyless]
    public class PeerReviewResponseDto
    {
        public string? RespnsArea { get; set; }
        public string? Language { get; set; }
        public bool Coordinator { get; set; }
        public bool TL { get; set; }
        public bool Judge { get; set; }
        public bool Trans { get; set; }
    }
}
