using FoodSuit_Backend.Inventory.Domain.Model.Aggregates;
using FoodSuit_Backend.Inventory.Domain.Repositories;
using FoodSuit_Backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using FoodSuit_Backend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FoodSuit_Backend.Inventory.Infrastructure.Persistence.EFC.Repositories;

public class ItemRepository(AppDbContext context) : BaseRepository<Item>(context), IItemRepository
{
    public new async Task<Item?> FindByIdAsync(int id)
    {
        return await Context.Set<Item>()
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<IEnumerable<Item>> FindByNameAsync(string name)
    {
        return await Context.Set<Item>().Where(i => i.Name == name).ToListAsync();
    }

    public async Task UpdateAsync(Item item)
    {
        Context.Set<Item>().Update(item);
        await Context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Item>> FindAllAsync()
    {
        return await Context.Set<Item>().ToListAsync();
    }
}