using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNDSystem.Core.Domain.Entities
{
    [Table("wkt_soi_example")]
    public class WktSoiExample
    {
        [Column("id")] public required int Id { get; set; }
    }
}
