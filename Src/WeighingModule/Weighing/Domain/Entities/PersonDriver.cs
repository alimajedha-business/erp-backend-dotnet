using General.Domain.Entities;
using Shared.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weighing.Domain.Common;
using Weighing.Domain.Enums;

namespace Weighing.Domain.Entities
{
    [Table("PersonDriver", Schema = "Weighing")]
    public class PersonDriver:BaseEntity
    {        
        public int PersonId { get; set; }
       
        public Person Person { get; set; } = null!;
        
        public int VehicleTypeId { get; set; }
              
        public string VehicleName { get; set; } = null!;
        
        public string PlateNumber { get; set; } = null!;

        public decimal? InitialWeight { get; set; }
        
        public DriverType DriverType { get; set; }
    }
}
