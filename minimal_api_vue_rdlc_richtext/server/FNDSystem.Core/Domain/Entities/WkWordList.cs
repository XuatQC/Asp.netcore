using System.ComponentModel.DataAnnotations.Schema;

namespace FNDSystem.Core.Domain.Entities
{
    [Table("tbl_wk_wordlist")]
    public class WkWordList
    {
        [Column("id")] public required int Id { get; set; }
        [Column("Num")] public string? Num { get; set; }
        [Column("fields")] public string? Fields { get; set; }
        [Column("title")] public string? Title { get; set; }
        [Column("newest_flag")] public bool NewestFlag { get; set; }
        [Column("delete_flag")] public bool DeleteFlag { get; set; }
        [Column("print_order")] public int PrintOrder { get; set; }
        [Column("pre_change_order")] public int PreChangeOrder { get; set; }
        [Column("after_change_order")] public int AfterChangeOrder { get; set; }
        [Column("output_target")] public bool OutputTarget { get; set; }
        [Column("package_exclude")] public bool PackageExclude { get; set; }
    }
}
