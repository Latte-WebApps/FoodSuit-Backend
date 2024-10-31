namespace FoodSuit_Backend.Inventory.Domain.Model.Commands;

public record UpdateItemCommand(string Name, int Quantity, string Image);