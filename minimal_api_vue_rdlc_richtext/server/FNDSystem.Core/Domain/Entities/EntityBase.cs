using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FNDSystem.Core.Domain.Entities
{
    public class EntityBase
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }
        [Column("created_at")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]

        public DateTime CreatedAt { get; set; }
        [Column("updated_at")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }
        [Column("created_by")]
        [MaxLength(50)]
        [JsonIgnore]

        public string? CreatedBy { get; set; }
        [Column("updated_by")]
        [MaxLength(50)]
        [JsonIgnore]

        public string? UpdatedBy { get; set; }
    }
}