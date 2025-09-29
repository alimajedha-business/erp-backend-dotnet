// Ignore Spelling: HCM

using General.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Domain.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public Company Company { get; set; } = null!;

        public int CreatorId { get; set; }

        [ForeignKey("CreatoreId")]
        public User Creator { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
    }
}
