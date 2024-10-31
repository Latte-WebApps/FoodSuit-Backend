using FoodSuit_Backend.Inventory.Domain.Model.Commands;
using FoodSuit_Backend.Inventory.Interfaces.Rest.Resources;

namespace FoodSuit_Backend.Inventory.Interfaces.REST.Transform.Transform;

public static class CreateItemCommandFromResourceAssembler
{
    /// <summary>
    /// Transform a CreateFavoriteSourceResource to a CreateFavoriteSourceCommand 
    /// </summary>
    /// <param name="resource">The <see cref="CreateItemResource"/> resource</param>
    /// <returns>An instance of <see cref="CreateItemCommand"/></returns>
    public static CreateItemCommand ToCommandFromResource(CreateItemResource resource)
    {
        return new CreateItemCommand(resource.Name, resource.Quantity, resource.Image);
    }
}