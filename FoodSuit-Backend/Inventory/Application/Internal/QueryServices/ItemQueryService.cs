using FoodSuit_Backend.Inventory.Domain.Model.Aggregates;
using FoodSuit_Backend.Inventory.Domain.Model.Queries;
using FoodSuit_Backend.Inventory.Domain.Repositories;
using FoodSuit_Backend.Inventory.Domain.Services;

namespace FoodSuit_Backend.Inventory.Application.Internal.QueryServices;

public class ItemQueryService(IItemRepository itemRepository) : IItemQueryService
{

    public async Task<Item?> Handle(GetItemByIdQuery query)
    {
        return await itemRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<Item>> Handle(GetAllItemsQuery query)
    {
        return await itemRepository.FindAllAsync();
    }
}