using NGErp.General.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGErp.General.Service.Interfaces.Services
{
    public interface IDomainService
    {
        Task<DomainDto?> GetDomainAsync(Guid domainId);
    }
}
