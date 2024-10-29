namespace FoodSuit_Backend.Inventory.Domain.Model.Commands;

public record UpdateItemCommand(int Id,string Name, int Quantity, string Image);