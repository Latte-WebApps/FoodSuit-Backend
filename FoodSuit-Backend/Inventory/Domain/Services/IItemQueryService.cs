using FoodSuit_Backend.Inventory.Domain.Model.Aggregates;
using FoodSuit_Backend.Inventory.Domain.Model.Queries;

namespace FoodSuit_Backend.Inventory.Domain.Services;

public interface IItemQueryService
{
    Task<Item?> Handle(GetItemByIdQuery query);
    Task<IEnumerable<Item>> Handle(GetAllItemsQuery query);

}