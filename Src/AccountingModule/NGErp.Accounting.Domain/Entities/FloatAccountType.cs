using System.ComponentModel.DataAnnotations.Schema;

namespace NGErp.Accounting.Domain.Entities;

[Table("float_account_types", Schema = "Accounting")]
public class FloatAccountType
{
    public Guid Id { get; set; }
    public string NameFa { get; private set; } = default!;
    public string NameEn { get; private set; } = default!;
    public bool IsStatic { get; private set; }
    public int VoucherItemFloatLevel { get; private set; }
    public Guid? ParentId { get; private set; }

    public FloatAccountType? Parent { get; set; }
}
