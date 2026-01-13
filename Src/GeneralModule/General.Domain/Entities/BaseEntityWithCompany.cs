using System.ComponentModel.DataAnnotations.Schema;

namespace NGErp.General.Domain.Entities;

public class BaseEntityWithCompany : BaseEntity
{
    [ForeignKey(nameof(Company))]
    public Guid CompanyId { get; private set; }
}
