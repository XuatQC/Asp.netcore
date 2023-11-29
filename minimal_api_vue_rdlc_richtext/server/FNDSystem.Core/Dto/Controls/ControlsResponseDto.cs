using Microsoft.EntityFrameworkCore;

namespace FNDSystem.Core.Dto
{
    [Keyless]
    public class ControlsResponseDto
    {
        public DateTime? PlantName { get; set; }
        public DateTime? Lang { get; set; }
    }
}
