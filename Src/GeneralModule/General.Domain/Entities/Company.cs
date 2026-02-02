using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NGErp.Base.Domain.Entities;

namespace NGErp.General.Domain.Entities
{
    [Table("Companies",Schema ="General")]
    public class Company 
    {
        public Guid Id { get; set; }

        public string Name { get; private set; } = default!;

    }
}