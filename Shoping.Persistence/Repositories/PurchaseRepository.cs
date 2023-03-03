using Microsoft.EntityFrameworkCore;
using Shoping.Application.Common.Interfaces.Repositories;
using Shoping.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Persistence.Repositories
{
    public class PurchaseRepository : GenericRepository<Purchase>, IPurchaseRepository
    {
        // private readonly DbSet<Purchase> _entitiySet;

        public PurchaseRepository(AppDbContext dbContext) : base(dbContext)
        {
            // _entitiySet = _dbContext.Set<Purchase>();
        }

        //public async Task<IEnumerable<Purchase>> GetAllIncludingAsync(params Expression<Func<Purchase, object>>[] includes)
        //{
        //    IQueryable<Purchase> query = _entitiySet;
        //    return await includes.Aggregate(query, (current, include) => current.Include(include)).ToListAsync();
        //}
    }
}
