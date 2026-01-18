using System.Reflection;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

using NGErp.Base.Domain.Entities;

namespace NGErp.Base.Infrastructure.DataAccess
{
    public class ApplicationContext : DbContext
    {
        private Type _domainType;
        private Type[]? _domainTypes;

        public ApplicationContext(DbContextOptions options, Type domainType, Type[]? domainTypes = null) : base(options)
        {
            _domainType = domainType;
            _domainTypes = domainTypes;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = Assembly.GetAssembly(_domainType);
            if (assembly == null)
                return;

            var lstofmapClass = assembly.GetTypes().
                Where(x => x.IsAssignableToGenericType(typeof(IBaseEntityTypeConfiguration<>)) ||
                x.IsAssignableToGenericType(typeof(IViewModelTypeConfiguration<>))).ToList();

            if (_domainTypes != null)
                foreach (var t in _domainTypes)
                {
                    var lst = Assembly.GetAssembly(t)!.GetTypes()
                        .Where(x => x.IsAssignableToGenericType(typeof(IBaseEntityTypeConfiguration<>)) ||
                        x.IsAssignableToGenericType(typeof(IViewModelTypeConfiguration<>))).ToList();
                    lstofmapClass.AddRange(lst);
                }

            foreach (var maptype in lstofmapClass)
            {
                var constructor = maptype.GetConstructor(Array.Empty<Type>());
                if (constructor == null)
                    continue;

                var mapobj = constructor.Invoke(Array.Empty<object>());
                if (mapobj == null)
                    continue;

                var methodinfo = maptype.GetMethod("Map");
                if (methodinfo == null)
                    continue;

                var parameters = methodinfo.GetParameters();
                if (parameters == null || parameters.Length == 0)
                    continue;

                var EntityMethodInfo = modelBuilder.GetType().GetMethod("Entity", Array.Empty<Type>());
                if (EntityMethodInfo == null)
                    continue;

                var genericType = parameters[0].ParameterType.GenericTypeArguments;
                if (genericType == null || genericType.Length == 0)
                    continue;

                EntityMethodInfo = EntityMethodInfo.MakeGenericMethod(new Type[] { genericType[0] });
                var entity = EntityMethodInfo.Invoke(modelBuilder, Array.Empty<object>());
                if (entity == null)
                    continue;

                methodinfo.Invoke(mapobj, [entity]);
                if (maptype.IsSubclassOf(typeof(BaseEntity)) && mapobj is BaseEntity baseEntity)
                    BaseEntity.MapBase(modelBuilder.Entity(maptype.FullName!));
            }
        }

        public virtual System.Threading.Tasks.Task CommitAsync(IDbContextTransaction tran)
        {
            return tran.CommitAsync();
        }
        public virtual System.Threading.Tasks.Task RollbackAsync(IDbContextTransaction tran)
        {
            return tran.RollbackAsync();

        }
        public virtual System.Threading.Tasks.Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return this.Database.BeginTransactionAsync();
        }
    }
}
