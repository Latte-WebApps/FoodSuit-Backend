using FoodSuit_Backend.Inventory.Domain.Model.Aggregates;
using FoodSuit_Backend.Inventory.Domain.Model.Commands;

namespace FoodSuit_Backend.Inventory.Domain.Services;

public interface IItemCommandService
{
    Task<Item?> Handle(CreateItemCommand command);
    Task<Item?> Handle(UpdateItemCommand command);
    Task<bool?> Handle(DeleteItemCommand command);
    
}