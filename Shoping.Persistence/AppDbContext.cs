
using Shoping.Application.Common.Interfaces;
using Shoping.Domain;
using Shoping.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Shoping.Application.Common.Interfaces.Repositories;

namespace Shoping.Persistence;
public class AppDbContext : IdentityDbContext<User>
{
    private readonly CurrentUser _user;

    public AppDbContext(
        DbContextOptions<AppDbContext> options,
        ICurrentUserService currentUserService) : base(options)
    {
        _user = currentUserService.User;
    }
    
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Sale> Sales => Set<Sale>();
    public DbSet<SaleDetail> SalesDetail => Set<SaleDetail>();
    public DbSet<Purchase> Purchases => Set<Purchase>();
    public DbSet<PurchaseDetail> PurchasesDetail => Set<PurchaseDetail>();
    public DbSet<Provider> Providers => Set<Provider>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Inventary> Inventaries => Set<Inventary>();
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<DeliveryDetail> DeliverysDetail => Set<DeliveryDetail>();
    public DbSet<Delivery> Deliveries => Set<Delivery>();


    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = _user.Id;
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = _user.Id;
                    entry.Entity.LastModifiedByAt = DateTime.UtcNow;

                    if (entry.Entity.IsDeleted == true )
                    {
                        entry.Entity.DeletedBy = _user.Id;
                        entry.Entity.DeletedAt = DateTime.UtcNow;
                    }

                    if (entry.Entity.IsCanceled == true)
                    {
                        entry.Entity.CanceledBy = _user.Id;
                        entry.Entity.CanceledAt = DateTime.UtcNow;
                    }

                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
 
        base.OnModelCreating(builder);
    }
}
