namespace FNDSystem.Core.Dto
{
    public class PeerReviewDto
    {
        public int Id { get; set; }
        public int Years { get; set; }
        public int HoldNum { get; set; }
        public string? ReviewName { get; set; }
        public string? PlantName { get; set; }
        public bool Hold { get; set; }
        public bool PastPrSubject { get; set; }
        public string? DmDivision { get; set; }
        public string? OfferDivision { get; set; }
        public bool SyncSubjectLive { get; set; }
        public bool SyncSubjectPast { get; set; }
        public string? FollowUp { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
