using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace FNDSystem.Core.Dto
{
    [Keyless]
    public class ReviewMemberEditorResponseDto
    {
        [Column("initial")]
        public string? Initial { get; set; } = null;

        [Column("name")]
        public string? Name { get; set; } = null;

        [Column("tl_judge")]
        public string? TLJudge { get; set; } = null;

        [Column("comment")]
        public string? Comment { get; set; } = null;

        [Column("respns_area")]
        public string? ResponseArea { get; set; } = null;
    }
}
