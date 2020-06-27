using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProjectRootNamespace.Api.DataAccess.Entities;

namespace ProjectRootNamespace.Api.DataAccess.Seed
{
    // Development Seed Data
    public class DevSeedDataGenerator
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            // TODO: Place your seed data here
            var products = new List<Product>();

            var rnd = new Random();

            for (int i = 1; i <= 100; i++)
            {
                products.Add(new Product()
                {
                    Id = i,
                    Name = $"Product #{i}",
                    Description = $"Description for product #{i}",
                    Price = rnd.NextDouble() * rnd.Next(1, 1000),
                    Stock = rnd.Next(1, 10)
                });
            }

            modelBuilder.Entity<Product>().HasData(products);
            // Sets the start value for identity generated values
            // For details check: https://stackoverflow.com/questions/60187536/after-seeding-data-in-efcore-3-1-1-code-first-is-it-possible-to-set-identity-va)
            modelBuilder.Entity<Product>().Property(x => x.Id).HasIdentityOptions(products.Count + 1);
        }
    }
}
