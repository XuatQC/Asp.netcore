namespace FNDSystem.Core.Dto
{
    public class FindAccountRequestDto
    {
        public int PageNo { get; set; }
        public string? TenantCode { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
}