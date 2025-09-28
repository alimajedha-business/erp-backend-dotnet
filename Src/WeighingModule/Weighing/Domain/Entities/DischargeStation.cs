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
    [Table("DischargeStation", Schema = "Weighing")]
    public partial class DischargeStation: BaseEntity
    {
              
        [StringLength(10)]
        public string Code { get; set; } = null!;
        
        [StringLength(100)]
        public string Name { get; set; } = null!;

     
    }
}
