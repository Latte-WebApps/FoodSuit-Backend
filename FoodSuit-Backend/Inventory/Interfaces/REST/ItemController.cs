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
    public async Task<IActionResult> UpdateItem(int id, UpdateItemResource resource)
    {
        if (id <= 0 || resource is null) 
            return BadRequest("Invalid ID or resource is null.");

        try
        {
            var updateItemCommand = UpdateItemCommandFromResourceAssembler.ToCommandFromResource(resource);
            var item = await itemCommandService.Handle(id, updateItemCommand);

            if (item == null)
                return NotFound($"Item not found with id: {id}");

            var itemResource = ItemResourceFromEntityAssembler.ToResourceFromEntity(item);
            return Ok(itemResource);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
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
        