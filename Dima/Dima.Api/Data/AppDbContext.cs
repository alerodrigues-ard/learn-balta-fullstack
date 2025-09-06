using Microsoft.EntityFrameworkCore;
using Dima.Core.Models;

namespace Dima.Api.Data;

public class AppDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Transaction> Transactions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder) 
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration();
    }
}