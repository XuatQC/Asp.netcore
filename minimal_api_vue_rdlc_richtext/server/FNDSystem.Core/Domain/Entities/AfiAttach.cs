using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FNDSystem.Core.Domain.Entities
{
    [Table("tbl_afi_attach")]
    public partial class AfiAttach
    {
        [Column("id")] public required int Id { get; set; }
        [Column("num")][MaxLength(5000)] public  string? Num { get; set; }
        [Column("serial_num")][MaxLength(500)] public  string? SerialNum { get; set; }
        [Column("seq_num")][MaxLength(500)] public  string? SeqNum { get; set; }
        [Column("represent_phot")][MaxLength(500)] public  string? RepresentPhot { get; set; }
        [Column("file_name")] public  string? FileName { get; set; }
        [Column("size")][MaxLength(10000)] public  string? Size { get; set; }
        [Column("state_flag")][MaxLength(500)] public  string? StateFlag { get; set; }
        [Column("last_update")] public  DateTime LastUpdate { get; set; }

    }
}
