using Shoping.Application.Common.Interfaces;
using Shoping.Application.Common.Interfaces.Repositories;
using Shoping.Application.Common.Repositories;
using Shoping.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _dbContext;
       /*  private BaseRepository<Position> _positions;
        private BaseRepository<Client> _category; */
        private IGenericRepository<Category> _category;
        private IGenericRepository<Client> _client;
        private IGenericRepository<DeliveryDetail> _deliveyDetail;
        private IGenericRepository<Delivery> _delivey;


        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IGenericRepository<Inventary> Inventary => throw new NotImplementedException();

        IGenericRepository<Category> IUnitOfWork.Category => _category ?? (_category = new GenericRepository<Category>(_dbContext));
        IGenericRepository<Client> IUnitOfWork.Client => _client ?? (_client = new GenericRepository<Client>(_dbContext));
        IGenericRepository<DeliveryDetail> IUnitOfWork.DeliveryDetail => _deliveyDetail ?? (_deliveyDetail = new GenericRepository<DeliveryDetail>(_dbContext));
        IGenericRepository<Delivery> IUnitOfWork.Delivery => _delivey ?? (_delivey = new GenericRepository<Delivery>(_dbContext));

        public void Commit()
            => _dbContext.SaveChanges();


        public async Task CommitAsync()
            => await _dbContext.SaveChangesAsync();


        public void Rollback()
            => _dbContext.Dispose();


        public async Task RollbackAsync()
            => await _dbContext.DisposeAsync();
    }
}
