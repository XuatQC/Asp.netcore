using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Core.Entities
{
    [Table("tenants")]
    public partial class Tenant: EntityBase
    {
        [Column("code")]
        [MaxLength(10)]
        public required string Code { get; set; }
        [Column("name")]
        [MaxLength(80)]
        public required string Name { get; set; }

        [JsonIgnore]
        public ICollection<Account>? Accounts { get; set; }
    }
}