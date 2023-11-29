using FNDSystem.Core.Domain.Entities;

namespace FNDSystem.Core.Dto
{
    public class PrintTranRequesDto
    {
        public List<VwTranslateControl> preTranslateList { get; set; } = new List<VwTranslateControl>();
        public string Language { get; set; } = string.Empty;
        public ObsPathsDto obsPathsDto { get; set; } = new ObsPathsDto();
    }
}
