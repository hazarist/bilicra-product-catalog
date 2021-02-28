using Bilicra.ProductCatalog.Common.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bilicra.ProductCatalog.DataAccess.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<UserEntity> Users { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var entities = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedDate = DateTime.UtcNow;
                }
                ((BaseEntity)entity.Entity).LastUpdatedDate = DateTime.UtcNow;
            }

            return (await base.SaveChangesAsync(true, cancellationToken));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductEntity>().HasData(new ProductEntity
            {
                Id= Guid.Parse("b4512880-ecb2-444c-8c03-2908ab51892b"),
                Name = "test product",
                Code = "testCode",
                Currency = 0,
                IsConfirmed = true,
                Price = 55,
                CreatedDate = DateTime.Now
            },
            new ProductEntity
            {
                Id = Guid.Parse("698f932e-dcf6-4580-a5ca-5d572ade4765"),
                Name = "test product 2",
                Code = "testCode2",
                Currency = 0,
                IsConfirmed = false,
                Price = 1000,
                CreatedDate = DateTime.Now
            });

            modelBuilder.Entity<UserEntity>().HasData(new UserEntity
            {
                Id = Guid.Parse("b624b02c-4337-44fc-8aad-6bbefb7ed8e6"),
                Username = "test",
                Name =  "user",
                Surname = "test",
                Email = "user.test@gmail.com",
                Password = "OUQbMtDPGRPjcQlPLR0N+H4pwBj0RKx7sHH9lySmNww=",
                Salt = "KUDTfNFTrOMjdx5GtmpLYQ=="
            });

        }
    }
}
