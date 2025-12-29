using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace NGErp.Base.Domain.Entities
{
    public interface IBaseEntityTypeConfiguration<T> where T : BaseEntity
    {
        public void Map(EntityTypeBuilder<T> builder);
    }
    public interface IViewModelTypeConfiguration<T> where T : class
    {
        public void Map(EntityTypeBuilder<T> builder);
    }

}
