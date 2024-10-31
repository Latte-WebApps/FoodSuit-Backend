using FoodSuit_Backend.Inventory.Domain.Model.Aggregates;
using FoodSuit_Backend.Inventory.Interfaces.Rest.Resources;

namespace FoodSuit_Backend.Inventory.Interfaces.REST.Transform.Transform;

public static class ItemResourceFromEntityAssembler
{
    
    public static ItemResource ToResourceFromEntity(Item item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item), "Item cannot be null");
        }
        return new ItemResource(item.Id, item.Name, item.Quantity, item.Image);
    }
}