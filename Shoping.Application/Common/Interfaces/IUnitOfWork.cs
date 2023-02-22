using Shoping.Application.Common.Interfaces.Repositories;
using Shoping.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
       
       IGenericRepository<Category> Category { get; }
       IGenericRepository<Product> Product { get; }
       IGenericRepository<Sale> Sale { get; }
       IGenericRepository<Purchase> Purchase { get; }
       IGenericRepository<Provider> Provider { get; }
       IGenericRepository<PurchaseDetail> PurchaseDetail { get; }
       IGenericRepository<SaleDetail> SaleDetail { get; }

        void Commit();
        void Rollback();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
