using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Infrastructure.DataAccess.Repositories;
using NGErp.HCM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGErp.HCM.Infrastructure.DataAccess.Repositories
{
    public class PositionRepository : Repository<Position>, IPositionRepository
    {

        public PositionRepository(MainDbContext context) : base(context)
        {
        }
    }
}
