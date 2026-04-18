using System;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Infrastructure.config;

namespace Infrastructure.Data;

public class StoreContext(DbContextOptions options):DbContext(options)
{

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
    }


public DbSet<Product> Products { get; set; } 

}
