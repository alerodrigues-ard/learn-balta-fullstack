using System.Reflection;
using Dima.Api.Models;
using Microsoft.EntityFrameworkCore;
using Dima.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Dima.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) 
    : IdentityDbContext
    <
        User,                       // Tabela AspNetUsers 
        IdentityRole<long>,         // Tabela AspNetRoles (gera tabela de ligação AspNetUserRoles)
        long,
        IdentityUserClaim<long>,    // Tabela AspNetUserClaims
        IdentityUserRole<long>,     // Tabela AspNetUserRoles
        IdentityUserLogin<long>,    // Tabela AspNetUserLogins
        IdentityRoleClaim<long>,    // Tabela AspNetRoleClaims
        IdentityUserToken<long>     // Tabela AspNetUserTokens
    >(options)
{
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Transaction> Transactions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder) 
    {
        base.OnModelCreating(modelBuilder);
        
        // Aplica as configurações de todas as classes que implementam
        // IEntityTypeConfiguration dentro do Assembly
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}