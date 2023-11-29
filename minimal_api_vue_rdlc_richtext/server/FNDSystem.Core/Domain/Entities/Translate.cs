using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FNDSystem.Core.Domain.Entities
{
    [Table("tbl_translate")]
    [Keyless]
    public partial class Translate
    {
        [Column("num")][MaxLength(50)] public string Num { get; set; } = string.Empty;
        [Column("plant_name")][MaxLength(10)] public string? PlantName { get; set; }
        [Column("offer_num")] public int OfferNum { get; set; }
        [Column("title")] public string? Title { get; set; }
        [Column("title_trans")] public string? TitleTrans { get; set; }
        [Column("ver_trans_done")] public int? VerTransDone { get; set; }
        [Column("ver_original")] public int? VerOriginal { get; set; }
        [Column("editor")][MaxLength(3)] public string? Editor { get; set; }
        [Column("trans_situation")][MaxLength(10)] public string? TransSituation { get; set; } = string.Empty;
        [Column("trans_arrival")][MaxLength(1)] public string? TransArrival { get; set; } = string.Empty;
        [Column("trans_cancel")][MaxLength(3)] public string? TransCancel { get; set; }
        [Column("trans_urgent")][MaxLength(3)] public string? TransUrgent { get; set; }
        [Column("contact_memo")][MaxLength(255)] public string? ContactMemo { get; set; }
        [Column("output_date")] public DateTime? OutputDate { get; set; }
        [Column("capture_date")] public DateTime? CaptureDate { get; set; }
        [Column("trans_date")] public DateTime? TransDate { get; set; }
        [Column("translator")][MaxLength(20)] public string? Translator { get; set; }
        [Column("cancel_status")][MaxLength(10)] public string? CancelStatus { get; set; }
        [Column("trans_scope")][MaxLength(10)] public string? TransScope { get; set; }
        [Column("trans_deadline")][MaxLength(30)] public string? TransDeadline { get; set; }
        [Column("trans_lang")][MaxLength(1)] public string? TransLang { get; set; }
        [Column("trans_req_date")] public DateTime? TransReqDate { get; set; }
        [Column("file_name")][MaxLength(255)] public string? FileName { get; set; }
        [Column("select_check")] public bool? SelectCheck { get; set; }
        [Column("last_update")] public DateTime? LastUpdate { get; set; }
        [Column("last_user")][MaxLength(3)] public string? LastUser { get; set; }
    }
}
