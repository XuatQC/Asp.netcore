using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNDSystem.Core.Domain.Entities
{
    [Table("wkt_analysis_gfa_examples")]
    public class WktAnalysisGfaExamples
    {
        [Column("id")] public required int Id { get; set; }
    }
}
