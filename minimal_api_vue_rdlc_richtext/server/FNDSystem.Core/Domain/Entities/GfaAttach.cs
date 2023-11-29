using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FNDSystem.Core.Domain.Entities
{
    [Table("tbl_gfa_attach")]
    public partial class GfaAttach
    {
        [Column("id")] public required int Id { get; set; }
        [Column("num")][MaxLength(5000)] public string Num { get; set; } = string.Empty;
        [Column("serial_num")][MaxLength(500)] public string SerialNum { get; set; } = string.Empty;
        [Column("seq_num")][MaxLength(500)] public string SeqNum { get; set; } = string.Empty;
        [Column("represent_phot")][MaxLength(500)] public string RepresentPhot { get; set; } = string.Empty;
        [Column("file_name")] public string FileName { get; set; } = string.Empty;
        [Column("size")][MaxLength(1000)] public string Size { get; set; } = string.Empty;
        [Column("state_flag")][MaxLength(500)] public string StateFlag { get; set; } = string.Empty;
        [Column("last_update")] public DateTime LastUpdate { get; set; }
    }
}
