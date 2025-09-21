using General.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weighing.Domain.Common;

namespace Weighing.Domain.Entities
{
    /// <summary>
    /// جایگاه تخلیه
    /// </summary>
    [Table("discharge_stations", Schema = "weighing")]
    public partial class DischargeStation: BaseEntity
    {
      

        [Column("code")]
        [StringLength(10)]
        public string Code { get; set; } = null!;

        [Column("name")]
        [StringLength(100)]
        public string Name { get; set; } = null!;

     
    }
}
