using Common.Application.Interfaces;
using Common.Infrastructure.DataAccess;
using HCM.Application.Interfaces.Repositories;
using HCM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Infrastructure.DataAccess.Repositories
{
    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(IMainDbContext context) : base(context)
        {

        }
    }
}
