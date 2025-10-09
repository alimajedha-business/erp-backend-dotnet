// Ignore Spelling: HCM

using General.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Domain.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public int CreatorId { get; set; }

        public DateTime CreatedAt { get; set; }

        public int? ModifierId { get; set; }

        public DateTime? ModifiedAt { get; set; }

        // Navigation properties 
        public virtual Company Company { get; set; } = null!;
        public virtual User Creator { get; set; } = null!;
        public virtual User Modifier { get; set; } = null!;
    }
}
