using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using General.Domain.Entities;
using Shared.Domain.Entities;

namespace Warehouse.Domain.Entities
{
    [Table("WarehouseStocks", Schema = "Warehouse")]
    //[Index("CompanyId", Name = "WarehouseStocks_CompanyId")]
    //[Index("WarehouseTypeId", Name = "WarehouseStocks_WarehouseTypeId")]
    //[Index("BranchId", Name = "WarehouseStocks_BranchId")]
    public class WarehouseStock
    {
        [Key]
        public required long Id { get; set; }

        public required int WarehouseCode { get; set; }
     
        [StringLength(150)]
        public required string WarehouseName { get; set; }

    //    [Column(TypeName ="decimal(18,0)")]
    //    public decimal MaxAssetValue { get; set; }

    //    [ForeignKey("BranchId")]
    //    public int? BranchId { get; set; }
    //    public virtual ICollection<CompanyUnit> company_units { get; set; } = new List<CompanyUnit>();

    //    [ForeignKey("WarehouseTypeId")]
    //    public int? WarehouseTypeId { get; set; }
    //    public virtual WarehouseType WarehouseTypes { get; set; } = null!;

    //    [ForeignKey("CompanyId")]
    //    public int CompanyId { get; set; }
    //    public virtual Company Company { get; set; } = null!;

    //    public int? WarehouseMasterAccount { get; set; }
    //    public int? WarehouseSlaveAccount { get; set; }
    //    public int? WarehouseDetail1Account { get; set; }
    //    public int? WarehouseDetail2Account { get; set; }
    //    public bool  WarehouseAccountFixed { get; set; }


    //    public int? SalesMasterAccount { get; set; }
    //    public int? SalesSlaveAccount { get; set; }
    //    public int? SalesDetail1Account { get; set; }
    //    public int? SalesDetail2Account { get; set; }
    //    public bool SalesAccountIsStatic { get; set; }


    //    public int? ExportMasterAccount { get; set; }
    //    public int? ExportSlaveAccount { get; set; }
    //    public int? ExportDetail1Account { get; set; }
    //    public int? ExportDetail2Account { get; set; }
    //    public bool ExportAccountIsStatic { get; set; }

    //    public int? ReturnMasterAccount { get; set; }
    //    public int? ReturnSlaveAccount { get; set; }
    //    public int? ReturnDetail1Account { get; set; }
    //    public int? ReturnDetail2Account { get; set; }
    //    public bool ReturnAccountIsStatic { get; set; }

    //    public int? PurchaseReturnMasterAccount { get; set; }
    //    public int? PurchaseReturnSlaveAccount { get; set; }
    //    public int? PurchaseReturnDetail1Account { get; set; }
    //    public int? PurchaseReturnDetail2Account { get; set; }
    //    public bool PurchaseReturnAccountIsStatic { get; set; }
       
    }
}
