namespace FoodSuit_Backend.Inventory.Domain.exceptions;

public class ItemNotFoundException : Exception
{
    public ItemNotFoundException(string message) : base(message) { }
}