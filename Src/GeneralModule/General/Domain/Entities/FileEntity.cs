using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("file_entities", Schema = "general")]
[Index("EntityTypeId", Name = "file_entities_entity_type_id_addc8bf3")]
[Index("FileId", Name = "file_entities_file_id_1c97e002")]
public partial class FileEntity
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("object_id")]
    public long? ObjectId { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("entity_type_id")]
    public int EntityTypeId { get; set; }

    [Column("file_id")]
    public int FileId { get; set; }

    [ForeignKey("EntityTypeId")]
    [InverseProperty("FileEntities")]
    public virtual EntityType EntityType { get; set; } = null!;

    [ForeignKey("FileId")]
    [InverseProperty("FileEntities")]
    public virtual File File { get; set; } = null!;
}
