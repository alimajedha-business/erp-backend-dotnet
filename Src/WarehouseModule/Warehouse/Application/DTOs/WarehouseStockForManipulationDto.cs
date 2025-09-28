using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Application.DTOs
{
    public abstract record WarehouseStockForManipulationDto
    {
        [Required(ErrorMessage = "Code is a required field.")]
        public int Code { get; set; }

  
        [Required(ErrorMessage = "Name is a required field.")]
        [MaxLength(150, ErrorMessage = "Maximum length for the Name is 150 characters.")]
        public required string Name { get; set; }


        [Required(ErrorMessage = "CompanyId is a required field.")]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "WarehouseTypeId is a required field.")]
        public int WarehouseTypeId { get; set; }
      //  public int? CompanyUnitId { get; set; }

        // public IEnumerable<WarehouseStockForCreationDto>? WarehouseType { get; init; }

        // public IEnumerable<WarehouseStockForManipulationDto>? Inputs { get; init; }
    }
}
