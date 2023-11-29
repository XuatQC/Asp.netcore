namespace FNDSystem.Core.Dto
{
    public class ObsSaveDto
    {
        public ObsRequestDto Obs { get; set; } = new ObsRequestDto();
        public List<ObsFactRequestDto> Facts { get; set; } = new List<ObsFactRequestDto>();
        public List<ObsConclusionRequestDto> Conclusions { get; set; } = new List<ObsConclusionRequestDto>();
        public bool IsNewFlg { get; set; } = false;
    }
}
