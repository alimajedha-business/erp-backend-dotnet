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
using General.Domain.Entities;

namespace Warehouse.Domain.Entities
{
    [Table("WarehouseStock", Schema = "warehouse")]
    [Index("CompanyId", Name = "WarehouseStock_CompanyId")]
    [Index("WarehouseTypeId", Name = "WarehouseStock_WarehouseTypeId")]
    [Index("CompanyUnitId", Name = "WarehouseStock_CompanyUnitId")]
    public class WarehouseStock
    {
        [Key]
        public long Id { get; set; }

        public required int CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public required virtual Company Company{ get; set; }

        public required int Code { get; set; }
     
        [StringLength(150)]
        public required string Name { get; set; }

      //  [Column("MaxAssetValue" = "decimal(18,0)")]
        public decimal MaxAssetValue { get; set; }
        

        public int? CompanyUnitId { get; set; }
        
        [ForeignKey("CompanyUnitId")]
        public virtual CompanyUnit? CompanyUnit { get; set; }
        

        public int? WarehouseTypeId { get; set; }

        [ForeignKey("WarehouseTypeId")]
        public virtual WarehouseType? WarehouseType { get; set; }
        


        public int? WarehouseMasterAccount { get; set; }
        public int? WarehouseSlaveAccount { get; set; }
        public int? WarehouseDetail1Account { get; set; }
        public int? WarehouseDetail2Account { get; set; }
        public bool WarehouseAccountIsStatic { get; set; }


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

        public int? ContractorMasterAccount { get; set; }
        public int? ContractorSlaveAccount { get; set; }
        public int? ContractorDetail1Account { get; set; }
        public int? ContractorDetail2Account { get; set; }
        public bool ContractorAccountIsStatic { get; set; }


    }
}
