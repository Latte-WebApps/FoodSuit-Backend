namespace FoodSuit_Backend.Inventory.Domain.Model.Commands;

public record CreateItemCommand(string Name, int Quantity, string Image)
{
   
}