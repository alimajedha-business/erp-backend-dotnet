using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Domain.Entities
{
    [Table("PayrollTests", Schema = "payroll")]
    public class PayrollTest
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("code")]
        public int Code { get; set; }

        [Column("name")]
        [StringLength(100)]
        public string Name { get; set; } = null!;
    }
}
