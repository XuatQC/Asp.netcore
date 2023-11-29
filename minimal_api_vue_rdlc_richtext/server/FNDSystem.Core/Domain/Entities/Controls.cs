using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FNDSystem.Core.Domain
{
    [Table("tbl_controls")]
    [PrimaryKey(nameof(LastSyncDate))]
    public partial class Controls
    {
        [Column("last_sync_date")] public DateTime LastSyncDate { get; set; }
        [Column("last_update")] public DateTime? LastUpdate { get; set; }
        [Column("last_user")][MaxLength(3)] public string? LastUser { get; set; }
    }
}
