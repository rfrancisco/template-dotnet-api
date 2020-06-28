﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using ProjectRootNamespace.Api.DataAccess.Seed;
using ProjectRootNamespace.Api.Infrastructure.DataAccess;

namespace ProjectRootNamespace.Api.DataAccess
{
    public class MainDbContext : BaseDbContext
    {
        // TODO: Register entities DbSets here

        public MainDbContext(
            DbContextOptions<MainDbContext> options,
            ILoggerFactory logger,
            IHostEnvironment env) : base(options, logger, env) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TODO: Call entities OnModelCreating methods here

            base.OnModelCreating(modelBuilder);

            switch (_environment.EnvironmentName)
            {
                case "Development":
                    DevSeedDataGenerator.SeedData(modelBuilder);
                    break;
                case "Staging":
                    StgSeedDataGenerator.SeedData(modelBuilder);
                    break;
                case "Production":
                    PrdSeedDataGenerator.SeedData(modelBuilder);
                    break;
                default:
                    throw new System.Exception("Unexpected environment: " + _environment.EnvironmentName);
            }
        }
    }
}
