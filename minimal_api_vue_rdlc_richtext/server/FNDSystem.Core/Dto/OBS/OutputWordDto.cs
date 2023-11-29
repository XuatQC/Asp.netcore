using FNDSystem.Core.Domain.Entities.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNDSystem.Core.Dto.OBS
{
    public class OutputWordDto
    {
        public string? Number { get; set; }
        public string? Area { get; set; }
        public string? Scope { get; set; }
        public string? Title { get; set; }
        public string? PlantName { get; set; }
        public bool? IsEnglish { get; set; }
        public string? Path { get; set; }
        public List<ObsFactReport>? Facts { get; set; }
        public List<ObsConclusionReport>? Conclusions { get; set; }
    }
}
