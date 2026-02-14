using System.ComponentModel.DataAnnotations.Schema;

namespace NGErp.General.Domain.Entities;

[Table("Companies",Schema ="General")]
public class Company 
{
    public Guid Id { get; set; }
    public string Name { get; private set; } = default!;
}