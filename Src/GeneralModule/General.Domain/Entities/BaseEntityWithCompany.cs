using System.ComponentModel.DataAnnotations.Schema;

using NGErp.Base.Domain.Entities;

namespace NGErp.General.Domain.Entities;

public class BaseEntityWithCompany : BaseEntity
{
    [ForeignKey(nameof(Company))]
    public Guid CompanyId { get; private set; }
}
