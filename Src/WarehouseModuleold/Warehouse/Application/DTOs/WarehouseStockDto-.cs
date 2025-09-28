using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Application.DTOs
{
    public record class WarehouseStockDto
    {
        [Key]
        public required int Id { get; init; }
        public int WarehouseCode { get; init; }
        public string WarehouseName { get; init; } = string.Empty;
        //public int CompanyId { get; init; }
        //public decimal MaxAssetValue { get; init; }
        //public int BranchId { get; init; }
        //public int WarehouseTypeId { get; init; }
        //public int WarehouseMasterAccount { get; init; }
        //public int WarehouseSlaveAccount { get; init; }
        //public int WarehouseDetail1Account { get; init; }
        //public int WarehouseDetail2Account { get; init; }
        //public bool WarehouseAccountFixed { get; init; }
        //public int SalesMasterAccount { get; init; }
        //public int SalesSlaveAccount { get; init; }
        //public int SalesDetail1Account { get; init; }
        //public int SalesDetail2Account { get; init; }
        //public bool SalesAccountIsStatic { get; init; }
        //public int ExportMasterAccount { get; init; }
        //public int ExportSlaveAccount { get; init; }
        //public int ExportDetail1Account { get; init; }
        //public int ExportDetail2Account { get; init; }
        //public bool ExportAccountIsStatic { get; init; }
        //public int ReturnMasterAccount { get; init; }
        //public int ReturnSlaveAccount { get; init; }
        //public int ReturnDetail1Account { get; init; }
        //public int ReturnDetail2Account { get; init; }
        //public bool ReturnAccountIsStatic { get; init; }
        //public int PurchaseReturnMasterAccount { get; init; }
        //public int PurchaseReturnSlaveAccount { get; init; }
        //public int PurchaseReturnDetail1Account { get; init; }
        //public int PurchaseReturnDetail2Account { get; init; }
        //public bool PurchaseReturnAccountIsStatic { get; init; }
    }
}
