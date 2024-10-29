using FoodSuit_Backend.Inventory.Domain.Model.Commands;

namespace FoodSuit_Backend.Inventory.Domain.Model.Aggregates;



/// Item Aggregate
/// <summary>
/// This class represents the Item aggregate. It is used to store the ingredientes. 
/// </summary>
/// 
public class Item 
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public int Quantity { get; private set; }
    public string Image { get; private set; }
    

    protected Item()
    {
        this.Name = string.Empty;
        this.Quantity = 0;
        this.Image = string.Empty;
    }

    /// <summary>
    /// Constructor for the Inventory aggregate
    /// </summary>
    /// <remarks>
    /// The constructor is the command handler for the CreateItemCommand. It initializes the Item aggregate 
    /// </remarks>
    /// <param name="command">The CreateFavoriteSourceCommand command</param>
    
    
    public Item(CreateItemCommand command)
    {
        this.Name = command.Name;
        this.Quantity = command.Quantity;
        this.Image = command.Image;
    }
 
    
    /// <remarks>
    /// The constructor is the command handler for the UpdateItemCommand. It updates item aggregate
    /// </remarks>
    /// <param name="command">The UpdateFavoriteSourceCommand command</param>
    
    public Item(UpdateItemCommand command)
    {
        this.Name = command.Name;
        this.Quantity = command.Quantity;
        this.Image = command.Image;
    }

    public void UpdateInformation(string name, int quantity, string image)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty or whitespace.");

        if (quantity < 0)
            throw new ArgumentException("Quantity cannot be negative.");

        Name = name;
        Quantity = quantity;
        Image = image;
    }

}