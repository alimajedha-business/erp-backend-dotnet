using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Shared.Domain.Entities;

namespace Warehouse.Domain.Entities
{
    [Table("WarehouseStocks", Schema = "Warehouse")]
    [Index("CompanyId", Name = "WarehouseStocks_CompanyId")]
    [Index("WarehouseTypeId", Name = "WarehouseStocks_WarehouseTypeId")]
    [Index("BranchId", Name = "WarehouseStocks_BranchId")]
    public class WarehouseStock
    {
        [Key]
        public long Id { get; set; }

        public int WarehouseCode { get; set; }
     
        [StringLength(150)]
        public required string WarehouseName { get; set; }

        [Column(TypeName ="decimal(18,0)")]
        public decimal MaxAssetValue { get; set; }
        
        public int? BranchId { get; set; }
        
        //[ForeignKey("BranchId")]
        //public virtual ICollection<CompanyUnit>? CompanyUnits { get; set; }
        
        public int? WarehouseTypeId { get; set; }

        [ForeignKey("WarehouseTypeId")]
        public virtual WarehouseType WarehouseType { get; set; } = null!;
        
        public int CompanyId { get; set; }

        //[ForeignKey("CompanyId")]
        //public virtual General.Domain.Entities.Company? Company { get; set; }

        public int? WarehouseMasterAccount { get; set; }
        public int? WarehouseSlaveAccount { get; set; }
        public int? WarehouseDetail1Account { get; set; }
        public int? WarehouseDetail2Account { get; set; }
        public bool  WarehouseAccountFixed { get; set; }


        public int? SalesMasterAccount { get; set; }
        public int? SalesSlaveAccount { get; set; }
        public int? SalesDetail1Account { get; set; }
        public int? SalesDetail2Account { get; set; }
        public bool SalesAccountIsStatic { get; set; }


        public int? ExportMasterAccount { get; set; }
        public int? ExportSlaveAccount { get; set; }
        public int? ExportDetail1Account { get; set; }
        public int? ExportDetail2Account { get; set; }
        public bool ExportAccountIsStatic { get; set; }

        public int? ReturnMasterAccount { get; set; }
        public int? ReturnSlaveAccount { get; set; }
        public int? ReturnDetail1Account { get; set; }
        public int? ReturnDetail2Account { get; set; }
        public bool ReturnAccountIsStatic { get; set; }

        public int? PurchaseReturnMasterAccount { get; set; }
        public int? PurchaseReturnSlaveAccount { get; set; }
        public int? PurchaseReturnDetail1Account { get; set; }
        public int? PurchaseReturnDetail2Account { get; set; }
        public bool PurchaseReturnAccountIsStatic { get; set; }
       
    }
}
