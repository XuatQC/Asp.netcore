using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Core.Entities
{
    [Table("accounts")]
    public partial class Account: EntityBase
    {
        [Column("tenant_code")]
        [MaxLength(10)]

        public required string TenantCode { get; set; }
        [Column("code")]
        [MaxLength(15)]

        public required string Code { get; set; }
        [Column("name")]
        [MaxLength(80)]

        public required string Name { get; set; }
        [Column("email")]
        [MaxLength(50)]
        public required string Email { get; set; }
        [Column("password")]
        [MaxLength(255)]
        [JsonIgnore]
        public string? Password { get; set; }

        [Column("role_kbn")]
        [MaxLength(2)]
        public required string RoleKbn { get; set;}

        public Tenant? Tenant { get; set; }
    }
}