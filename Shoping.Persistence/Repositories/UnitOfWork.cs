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
        private IGenericRepository<Product> _product;
        private IGenericRepository<Sale> _sale;
        private IGenericRepository<Purchase> _purchase;
        private IGenericRepository<Provider> _provider;
        private IGenericRepository<PurchaseDetail> _purchaseDetail;
        private IGenericRepository<SaleDetail> _saleDetail;
        private IGenericRepository<Client> _client;
        private IGenericRepository<DeliveryDetail> _deliveyDetail;
        private IGenericRepository<Delivery> _delivey;
        private IGenericRepository<Inventary> _inventary;
        private IGenericRepository<Delivery> _delivery;
        private IGenericRepository<DeliveryDetail> _deliveryDetail;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        IGenericRepository<Category> IUnitOfWork.Category => _category ?? (_category = new GenericRepository<Category>(_dbContext));
        IGenericRepository<Product> IUnitOfWork.Product => _product ?? (_product = new GenericRepository<Product>(_dbContext));
        IGenericRepository<Sale> IUnitOfWork.Sale => _sale ?? (_sale = new GenericRepository<Sale>(_dbContext));
        IGenericRepository<Purchase> IUnitOfWork.Purchase => _purchase ?? (_purchase = new GenericRepository<Purchase>(_dbContext));
        IGenericRepository<Provider> IUnitOfWork.Provider => _provider ?? (_provider = new GenericRepository<Provider>(_dbContext));
        IGenericRepository<PurchaseDetail> IUnitOfWork.PurchaseDetail => _purchaseDetail ?? (_purchaseDetail = new GenericRepository<PurchaseDetail>(_dbContext));
        IGenericRepository<SaleDetail> IUnitOfWork.SaleDetail => _saleDetail ?? (_saleDetail = new GenericRepository<SaleDetail>(_dbContext));
        IGenericRepository<Inventary> IUnitOfWork.Inventary => _inventary ?? (_inventary = new GenericRepository<Inventary>(_dbContext));
        IGenericRepository<Client> IUnitOfWork.Client => _client ?? (_client = new GenericRepository<Client>(_dbContext));
        IGenericRepository<DeliveryDetail> IUnitOfWork.DeliveryDetail => _deliveryDetail ?? (_deliveryDetail = new GenericRepository<DeliveryDetail>(_dbContext));
        IGenericRepository<Delivery> IUnitOfWork.Delivery => _delivery ?? (_delivery = new GenericRepository<Delivery>(_dbContext));

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
