using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using FoodSuit_Backend.Inventory.Domain.Model.Aggregates;
using FoodSuit_Backend.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using Microsoft.EntityFrameworkCore;

namespace FoodSuit_Backend.Shared.Infrastructure.Persistence.EFC.Configuration;



public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)

{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<Item>().HasKey(f => f.Id);
        builder.Entity<Item>().Property(f => f.Id)
            .IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Item>().Property(f => f.Name)
            .IsRequired();
        builder.Entity<Item>().Property(f => f.Quantity)
            .IsRequired();  
        builder.Entity<Item>().Property(f => f.Image)
            .IsRequired();
 
        builder.UseSnakeCaseNamingConvention();

    }
    
}