using System.ComponentModel.DataAnnotations.Schema;

namespace FNDSystem.Core.Domain.Entities
{
    [Table("wkt_transhistory")]
    public class WktTransHistory
    {
        [Column("id")] public int Id { get; set; }
        [Column("item")] public string? Item { get; set; }
        [Column("revisions")] public int Revisions { get; set; }
        [Column("japanese")] public string? Japanese { get; set; }
        [Column("english")] public string? English { get; set; }
        [Column("translated")] public string? Translated { get; set; }
        [Column("display_order")] public int? DisplayOrder { get; set; }
    }
}
