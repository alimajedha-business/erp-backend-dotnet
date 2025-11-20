using General.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Application.Interfaces.Repositories
{
    public interface IDomainRepository
    {
        Task<Domain.Entities.Domain?> GetDomainAsync(int domainId, bool trackChanges);
    }
}
