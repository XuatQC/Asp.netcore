namespace FNDSystem.Core.Dto
{
    public class RevieMemberDto
    {
        public int Id { get; set; }
        public int IdPant { get; set; }
        public string? PlantName { get; set; }
        public string? Initial { get; set; }
        public string? Name { get; set; }
        public string? RomaName { get; set; }
        public string? RespnsArea { get; set; }
        public string? Language { get; set; }
        public bool Coordinator { get; set; }
        public bool Tl { get; set; }
        public bool Judge { get; set; }
        public bool Editor { get; set; }
        public bool Trans { get; set; }
        public string? Judge1 { get; set; }
        public string? Judge2 { get; set; }
        public string? Judge3 { get; set; }
        public string? TlJudge { get; set; }
        public string? Comment { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
