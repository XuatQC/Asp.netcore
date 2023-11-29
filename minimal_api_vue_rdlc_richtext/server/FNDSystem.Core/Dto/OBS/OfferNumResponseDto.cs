using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNDSystem.Core.Dto
{
    [Keyless]
    public class OfferNumResponseDto
    {
        public string? OfferNum { get; set; }
    }
}
