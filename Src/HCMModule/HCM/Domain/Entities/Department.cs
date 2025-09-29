// Ignore Spelling: HCM

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Domain.Entities
{
    public class Department : BaseEntity
    {
        [MaxLength(10)]
        public string? Code { get; set; }

        [MaxLength(200)]
        public required string Name { get; set; }

        public string? Description { get; set; }

        public bool Status { get; set; } = true;

        public DateTime? StatusChangeDate { get; set; } 
    }
}
