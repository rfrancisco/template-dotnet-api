using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using projectRootNamespace.Api.DataAccess.Entities;

namespace projectRootNamespace.Api.DataAccess.Seed
{
    // Development Seed Data
    public class DevSeedDataGenerator
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            // TODO: Place your seed data here
            var products = new List<Product>();
            var skus = new List<Sku>();

            var rnd = new Random();

            for (int i = 1; i <= 100; i++)
            {
                products.Add(new Product()
                {
                    Id = i,
                    Name = $"Product #{i}",
                    Description = $"Description for product #{i}",
                    CreatedBy = "unknown",
                    CreatedOn = new DateTime(2020, 1, 1),
                    UpdatedBy = "unknown",
                    UpdatedOn = new DateTime(2020, 1, 1)
                });
            }

            for (int i = 1; i <= 1000; i++)
            {
                skus.Add(new Sku()
                {
                    Id = i,
                    Model = $"Model #{i}",
                    Price = rnd.NextDouble() * rnd.Next(1, 1000),
                    Stock = rnd.Next(1, 100000),
                    Size = (SkuSize)rnd.Next(0, 4),
                    ProductId = rnd.Next(1, products.Count),
                    CreatedBy = "unknown",
                    CreatedOn = new DateTime(2020, 1, 1),
                    UpdatedBy = "unknown",
                    UpdatedOn = new DateTime(2020, 1, 1)
                });
            }

            modelBuilder.Entity<Product>().HasData(products);
            modelBuilder.Entity<Sku>().HasData(skus);
            // Sets the start value for identity generated values
            // For details check: https://stackoverflow.com/questions/60187536/after-seeding-data-in-efcore-3-1-1-code-first-is-it-possible-to-set-identity-va)
            modelBuilder.Entity<Product>().Property(x => x.Id).HasIdentityOptions(products.Count + 1);
            modelBuilder.Entity<Sku>().Property(x => x.Id).HasIdentityOptions(skus.Count + 1);
        }
    }
}
