using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProjectName.Api.DataAccess.Entities;

namespace ProjectName.Api.DataAccess.Seed
{
    // Development Seed Data
    public class DevSeedDataGenerator
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            // TODO: Place your seed data here
            var products = new List<Product>();
            var rnd = new Random();

            for (int i = 0; i < 100; i++)
            {
                products.Add(new Product()
                {
                    Id = ((i + 1) * -1),
                    Name = "Product #" + (i + 1),
                    Description = "",
                    Price = rnd.NextDouble() * rnd.Next(1, 1000),
                    Stock = rnd.Next(1, 10)
                });
            }

            modelBuilder.Entity<Product>().HasData(products);
        }
    }
}
