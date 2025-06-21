using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("menu_items", Schema = "general")]
[Index("EntityTypeCommandId", Name = "menu_items_entity_type_command_id_4319057f")]
[Index("EntityTypeId", Name = "menu_items_entity_type_id_412db7e8")]
[Index("ModuleId", Name = "menu_items_module_id_56dd60a4")]
[Index("ParentId", Name = "menu_items_parent_id_7032fad2")]
public partial class MenuItem
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("order")]
    public short Order { get; set; }

    [Column("name_fa")]
    [StringLength(100)]
    public string NameFa { get; set; } = null!;

    [Column("name_en")]
    [StringLength(100)]
    public string NameEn { get; set; } = null!;

    [Column("link")]
    [StringLength(200)]
    public string? Link { get; set; }

    [Column("short_key")]
    [StringLength(25)]
    public string? ShortKey { get; set; }

    [Column("new_tab")]
    public bool NewTab { get; set; }

    [Column("standard_page")]
    [StringLength(1000)]
    public string? StandardPage { get; set; }

    [Column("meta")]
    [StringLength(200)]
    public string? Meta { get; set; }

    [Column("entity_type_id")]
    public int? EntityTypeId { get; set; }

    [Column("entity_type_command_id")]
    public int? EntityTypeCommandId { get; set; }

    [Column("module_id")]
    public int ModuleId { get; set; }

    [Column("parent_id")]
    public int? ParentId { get; set; }

    [ForeignKey("EntityTypeId")]
    [InverseProperty("MenuItems")]
    public virtual EntityType? EntityType { get; set; }

    [ForeignKey("EntityTypeCommandId")]
    [InverseProperty("MenuItems")]
    public virtual EntityTypeCommand? EntityTypeCommand { get; set; }

    [InverseProperty("Parent")]
    public virtual ICollection<MenuItem> InverseParent { get; set; } = new List<MenuItem>();

    [ForeignKey("ModuleId")]
    [InverseProperty("MenuItems")]
    public virtual Module Module { get; set; } = null!;

    [ForeignKey("ParentId")]
    [InverseProperty("InverseParent")]
    public virtual MenuItem? Parent { get; set; }
}
