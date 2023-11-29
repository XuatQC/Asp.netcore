using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FNDSystem.Core.Dto
{
    [Keyless]
    public class UserResponseDto
    {
        [Column("name")]
        [MaxLength(50)]
        public string? Name { get; set; }

        [Column("roma_name")]
        [MaxLength(50)]
        public string? RomaName { get; set; }
    }
}
