using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("companies", Schema = "general")]
[Index("DomainId", Name = "companies_domain_id_51b444a3")]
public partial class Company
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("name2")]
    [StringLength(100)]
    public string? Name2 { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("domain_id")]
    public int DomainId { get; set; }

    [InverseProperty("Company")]
    public virtual ICollection<CompanyAdmin> CompanyAdmins { get; set; } = new List<CompanyAdmin>();

    [InverseProperty("Company")]
    public virtual ICollection<CompanyModule> CompanyModules { get; set; } = new List<CompanyModule>();

    [ForeignKey("DomainId")]
    [InverseProperty("Companies")]
    public virtual Domain Domain { get; set; } = null!;

    [InverseProperty("Company")]
    public virtual ICollection<Log> Logs { get; set; } = new List<Log>();

    [InverseProperty("Company")]
    public virtual ICollection<SelectLog> SelectLogs { get; set; } = new List<SelectLog>();

    //public virtual ICollection<WarehouseStock> WarehouseStocks { get; set; } = new List<WarehouseStock>();






}
