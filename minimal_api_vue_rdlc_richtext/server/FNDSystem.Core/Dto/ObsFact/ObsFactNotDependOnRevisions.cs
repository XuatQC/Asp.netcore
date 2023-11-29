using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNDSystem.Core.Dto.ObsFact
{
    public class ObsFactNotDependOnRevisions
    {
        public string NumNoRevisions { get; set; } = string.Empty;
        public int FactNum { get; set; }
        public int MaxNum { get; set; } = 0;
    }
}
