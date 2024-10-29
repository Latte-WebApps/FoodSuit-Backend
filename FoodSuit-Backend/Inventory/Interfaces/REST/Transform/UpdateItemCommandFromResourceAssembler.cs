using FoodSuit_Backend.Inventory.Domain.Model.Commands;
using FoodSuit_Backend.Inventory.Interfaces.Rest.Resources;

namespace FoodSuit_Backend.Inventory.Interfaces.REST.Transform.Transform;

public static class UpdateItemCommandFromResourceAssembler
{
    public static UpdateItemCommand ToCommandFromResource(UpdateItemResource resource)
    {
        return new UpdateItemCommand(resource.Id, resource.Name, resource.Quantity, resource.Image); 
    }
}