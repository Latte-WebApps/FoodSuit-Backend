namespace FoodSuit_Backend.Inventory.Interfaces.Rest.Resources;

public record CreateItemResource(string Name, int Quantity, string Image);