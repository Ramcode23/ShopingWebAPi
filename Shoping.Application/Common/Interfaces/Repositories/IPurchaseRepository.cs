using Shoping.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Application.Common.Interfaces.Repositories
{
    public interface IPurchaseRepository : IGenericRepository<Purchase>
    {
        // Task<IEnumerable<Purchase>> GetAllIncludingAsync(params Expression<Func<Purchase, object>>[] includes);
    }
}
