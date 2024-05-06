using Microsoft.EntityFrameworkCore;
using ContosoPizza.Models;

namespace ContosoPizza.Data;

public class PizzaContext : DbContext
{
    public int TenantId { get; set; }

    public PizzaContext(DbContextOptions<PizzaContext> options)
        : base(options)
    {
    }

    public DbSet<Pizza> Pizzas => Set<Pizza>();
    public DbSet<Topping> Toppings => Set<Topping>();
    public DbSet<Sauce> Sauces => Set<Sauce>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pizza>().HasQueryFilter(p => p.TenantId == TenantId);
        modelBuilder.Entity<Topping>().HasQueryFilter(t => t.TenantId == TenantId);
        modelBuilder.Entity<Sauce>().HasQueryFilter(s => s.TenantId == TenantId);

        base.OnModelCreating(modelBuilder);
    }
}
