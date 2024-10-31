using FoodSuit_Backend.Inventory.Domain.Model.Aggregates;
using FoodSuit_Backend.Inventory.Domain.Model.Commands;
using FoodSuit_Backend.Shared.Domain.Repositories;

namespace FoodSuit_Backend.Inventory.Domain.Repositories;

public interface IItemRepository: IBaseRepository<Item>
{
    Task<Item?> FindByIdAsync(int id);
    Task<IEnumerable<Item>> FindByNameAsync(string name);
    Task UpdateAsync(Item item );
    Task<IEnumerable<Item>> FindAllAsync();


}