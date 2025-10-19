// Ignore Spelling: HCM

using HCM.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Infrastructure.DataAccess.Repositories
{
    public sealed class HCMRepositoryManager : IHCMRepositoryManager
    {
        private readonly HCMDbContext _dbContext;
        private readonly Lazy<IDepartmentRepository> _departmentRepository;
        private readonly Lazy<IPostRepository> _postRepository;

        public HCMRepositoryManager(HCMDbContext dbContext)
        {
            _dbContext = dbContext;

            _departmentRepository = new Lazy<IDepartmentRepository>(() =>
            new DepartmentRepository(_dbContext));

            _postRepository = new Lazy<IPostRepository>(() =>
            new PostRepository(_dbContext));
        }

        public IDepartmentRepository Department => _departmentRepository.Value;

        public IPostRepository Post => _postRepository.Value;

        public Task SaveAsync() => _dbContext.SaveChangesAsync();
    }
}
