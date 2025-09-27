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
    [Table("person_driver", Schema = "weighing")]
    public class PersonDriver:BaseEntity
    {
        [Column("person_id")]
        public int PersonId { get; set; }
        public Person Person { get; set; } = null!;

        [Column("vehicle_type_id")]
        public int VehicleTypeId { get; set; }
       // public VehicleType VehicleType { get; set; } = null!;
        [Column("vehicle_name")]
        public string VehicleName { get; set; } = null!;
        [Column("plate_number")]
        public string PlateNumber { get; set; } = null!;
        [Column("initial_weight")]
        public decimal? InitialWeight { get; set; }
        [Column("driver_type")]
        public DriverType DriverType { get; set; }
    }
}
