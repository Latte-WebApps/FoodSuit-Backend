using System.Net.Mime;
using FoodSuit_Backend.Inventory.Application.Internal.QueryServices;
using FoodSuit_Backend.Inventory.Domain.exceptions;
using FoodSuit_Backend.Inventory.Domain.Model.Commands;
using FoodSuit_Backend.Inventory.Domain.Model.Queries;
using FoodSuit_Backend.Inventory.Domain.Services;
using FoodSuit_Backend.Inventory.Interfaces.Rest.Resources;
using FoodSuit_Backend.Inventory.Interfaces.REST.Transform.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FoodSuit_Backend.Inventory.Interfaces.REST.Transform;

[ApiController]
[Route("/api/v1/[Controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class ItemController(
    IItemCommandService itemCommandService,
    IItemQueryService itemQueryService) : ControllerBase
{

    /// <summary>
    /// Create a new item
    /// </summary>
    /// <param name="resource">
    /// The <see cref="CreateItemResource"/> resource
    /// </param>
    /// <returns>
    /// The created item
    /// </returns>
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new item",
        Description = "Create a new item in the inventory",
        OperationId = "CreateItem")]
    [SwaggerResponse(StatusCodes.Status201Created, "The item was created", typeof(ItemResource))]
    public async Task<IActionResult> CreateItem(CreateItemResource resource)
    {
        var createItemCommand = CreateItemCommandFromResourceAssembler.ToCommandFromResource(resource);
        var item = await itemCommandService.Handle(createItemCommand);
        if (item is null) return BadRequest();
        var itemResource = ItemResourceFromEntityAssembler.ToResourceFromEntity(item);
        return CreatedAtAction(nameof(GetItemById), new { id = item.Id }, itemResource);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateItem(UpdateItemResource resource)
    {
        try
        {
            Console.WriteLine($"Received UpdateItem request for ID: {resource.Id}");
            Console.WriteLine($"Resource Data - Name: {resource.Name}, Quantity: {resource.Quantity}, Image: {resource.Image}");

            var updateItemCommand = UpdateItemCommandFromResourceAssembler.ToCommandFromResource(resource);

            var item = await itemCommandService.Handle(updateItemCommand);

            if (item is null)
            {
                Console.WriteLine("Item not found.");
                return NotFound($"Item with id {resource.Id} not found.");
            }

            return Ok(ItemResourceFromEntityAssembler.ToResourceFromEntity(item));
        }
        catch (ItemNotFoundException ex)
        {
            Console.WriteLine($"ItemNotFoundException: {ex.Message}");
            return NotFound($"Item with id {resource.Id} not found.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
            Console.WriteLine($"StackTrace: {ex.StackTrace}");
            return StatusCode(500, "Internal server error");
        }
    }


    [HttpGet]
    public async Task<IActionResult> GetItemById(int id)
    {
        var getItemByIdQuery = new GetItemByIdQuery(id);
        var result = await itemQueryService.Handle(getItemByIdQuery);
        var resource = ItemResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }
  
   [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteItem(int id)
    {
        try
        {
            var deleteItemCommand = new DeleteItemCommand(id);
            var itemDeleted = await itemCommandService.Handle(deleteItemCommand);

            if (itemDeleted is null)
                return NotFound($"Item with id {id} not found.");

            return Ok("Item deleted successfully!");
        }
        catch (ItemNotFoundException)
        {
            return NotFound($"Item with id {id} not found.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error");
        }
    }

}
        