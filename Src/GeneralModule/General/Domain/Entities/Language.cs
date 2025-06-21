using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("languages", Schema = "general")]
[Index("Code", Name = "UQ__language__357D4CF9757EF7F8", IsUnique = true)]
[Index("Name", Name = "UQ__language__72E12F1BD0EA1B2E", IsUnique = true)]
public partial class Language
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("code")]
    [StringLength(2)]
    public string Code { get; set; } = null!;

    [Column("direction")]
    [StringLength(3)]
    public string Direction { get; set; } = null!;

    [InverseProperty("Language")]
    public virtual ICollection<Domain> Domains { get; set; } = new List<Domain>();

    [InverseProperty("Language")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
