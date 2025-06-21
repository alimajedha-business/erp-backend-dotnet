using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("files", Schema = "general")]
[Index("Sha1", Name = "UQ__files__2FC437B2639CC3A2", IsUnique = true)]
[Index("Guid", Name = "UQ__files__497F6CB5C8798DFD", IsUnique = true)]
public partial class File
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("guid")]
    [StringLength(32)]
    public string Guid { get; set; } = null!;

    [Column("file")]
    [StringLength(100)]
    public string File1 { get; set; } = null!;

    [Column("filesize")]
    public long Filesize { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string? Name { get; set; }

    [Column("sha1")]
    [StringLength(42)]
    public string Sha1 { get; set; } = null!;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [InverseProperty("File")]
    public virtual ICollection<FileEntity> FileEntities { get; set; } = new List<FileEntity>();
}
