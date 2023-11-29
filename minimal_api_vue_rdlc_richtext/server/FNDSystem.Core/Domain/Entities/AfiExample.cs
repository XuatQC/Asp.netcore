using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FNDSystem.Core.Domain.Entities
{
    [Table("tbl_afi_example")]
    public partial class AfiExample
    {
        [Column("id")] public required int Id { get; set; }
        [Column("num")][MaxLength(50)] public string? Num { get; set; }
        [Column("quantity")] public int Quantity { get; set; }
        [Column("serial_num")][MaxLength(120)] public string? SerialNum { get; set; }
        [Column("example")] public string? Example { get; set; }
        [Column("example_trans")] public string? ExampleTrans { get; set; }
        [Column("judge")] public string? Judge { get; set; }
        [Column("part_trans")][MaxLength(120)] public string? PartTrans { get; set; }
        [Column("val_comp")][MaxLength(120)] public string? ValComp { get; set; }
        [Column("val_comp_lock")][MaxLength(120)] public string? ValCompLock { get; set; }
        [Column("seq_num")][MaxLength(160)] public string? SeqNum { get; set; }
        [Column("last_update")] public DateTime LastUpdate { get; set; }

    }
}
