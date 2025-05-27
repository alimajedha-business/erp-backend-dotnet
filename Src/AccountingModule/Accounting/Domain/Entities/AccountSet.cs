using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Domain.Entities
{
    [Table("account_sets")]
    public class AccountSet
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(200)]
        public string? Title { get; set; }
        [Column("company_id")]
        public int CompanyId { get; set; }
        [Column("ledger_id")]
        public int LedgerId { get; set; }
    }
}
