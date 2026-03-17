using System;
using Microsoft.EntityFrameworkCore;
using Core.Entities;

namespace Infrastructure.Data;

public class StoreContext(DbContextOptions options):DbContext(options)
{

public DbSet<Product> Products { get; set; }

}
