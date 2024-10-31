namespace FoodSuit_Backend.Inventory.Interfaces.Rest.Resources;

public record UpdateItemResource(string Name, int Quantity, string Image)
{
}