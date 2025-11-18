using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Application.Interfaces.Repositories
{
    public interface IHCMRepositoryManager
    {
        IDepartmentRepository Department { get; }

        IPostRepository Post { get; }

        Task SaveAsync();
    }
}
