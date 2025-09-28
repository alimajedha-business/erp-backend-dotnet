using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Application.DTOs
{
    public abstract record WarehouseTypeForManipulationDto
    {
        //فقط فیلدهای مشترک بین create,update باید باشد

        [Required(ErrorMessage = "TypeCode is a required field.")]
        public int TypeCode { get; set; }

        [Required(ErrorMessage = "TypeName is a required field.")]
        [StringLength(50)]
        public string TypeName { get; set; }=String.Empty;

        public IEnumerable<WarehouseTypeForManipulationDto>? WarehouseStocks { get; init; }
    }
}
