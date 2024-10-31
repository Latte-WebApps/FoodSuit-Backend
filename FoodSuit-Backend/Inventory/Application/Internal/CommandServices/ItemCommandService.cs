using FoodSuit_Backend.Inventory.Domain.exceptions;
using FoodSuit_Backend.Inventory.Domain.Model.Aggregates;
using FoodSuit_Backend.Inventory.Domain.Model.Commands;
using FoodSuit_Backend.Inventory.Domain.Repositories;
using FoodSuit_Backend.Inventory.Domain.Services;
using FoodSuit_Backend.Shared.Domain.Repositories;

namespace FoodSuit_Backend.Inventory.Application.Internal.CommandServices;

public class ItemCommandService(IItemRepository itemRepository, IUnitOfWork unitOfWork) : IItemCommandService
{
    public async Task<Item?> Handle(CreateItemCommand command)
    {
        var item = new Item(command);
        try
        {
            await itemRepository.AddAsync(item);
            await unitOfWork.CompleteAsync();
            return item;
        }
        catch (Exception e)
        {
            throw new Exception("Failed to create item", e);
        }
    }


    public async Task<Item?> Handle(int id, UpdateItemCommand command)
    {
        var item = await itemRepository.FindByIdAsync(id);

        if (item == null) {throw new Exception($"Item not found with id: {id}");}
        
        item.UpdateInformation(command.Name, command.Quantity, command.Image);
        
        try
        {
            await itemRepository.UpdateAsync(item);
            await unitOfWork.CompleteAsync();
            return item;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error updating item: {e.Message}");
            Console.WriteLine($"StackTrace: {e.StackTrace}");
            throw new Exception("Failed to update item", e);
        }
    }

    public async Task<bool?> Handle(DeleteItemCommand command)
    {
        var item = await itemRepository.FindByIdAsync(command.Id);
        if (item == null) { throw new ItemNotFoundException("Item not found"); }


        try
        {
            itemRepository.Remove(item);
            await unitOfWork.CompleteAsync();
            return true;
        }
        catch (Exception e)
        {
            throw new Exception("Failed to delete item", e);
        }
    }
}