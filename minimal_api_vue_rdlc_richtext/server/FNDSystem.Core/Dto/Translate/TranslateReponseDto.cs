using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FNDSystem.Core.Dto
{
    [Keyless]
    public class TranslateReponseDto
    {
        [Column("plant_name")][MaxLength(10)] public string? PlantName { get; set; }
        [Column("kinds")][MaxLength(20)] public string? Kinds { get; set; }
        [Column("fields")][MaxLength(4)] public string? Fields { get; set; }
        [Column("part_id")][MaxLength(3)] public string? PartId { get; set; }
        [Column("serial_branch_num")] public int? SerialBranchNum { get; set; }
        [Column("revisions")] public int? Revisions { get; set; }
        [Column("english_edition")][MaxLength(1)] public string? EnglishEdition { get; set; }
        [Column("title")] public string? Title { get; set; }
        [Column("title_trans")] public string? TitleTrans { get; set; }
        [Column("editor")][MaxLength(3)] public string? Editor { get; set; }
        [Column("approval_state_pr_rev")] public int? ApprovalStatePrRev { get; set; }
        [Column("trans_range")][MaxLength(10)] public string? TransRange { get; set; }
        [Column("trans_deadline")][MaxLength(30)] public string? TransDeadline { get; set; }
        [Column("trans_lang")][MaxLength(1)] public string? TransLang { get; set; }
        [Column("last_update")] public DateTime? LastUpdate { get; set; }
    }
}
