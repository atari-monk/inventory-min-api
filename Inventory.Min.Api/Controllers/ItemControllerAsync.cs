using AutoMapper;
using Inventory.Min.Api.Dto;
using Inventory.Min.Data;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Min.Api.Controllers;

[ApiController]
[Route("api/items")]
public class ItemControllerAsync
    : ControllerBase
{
    private readonly IInventoryUnitOfWork uow;
    private readonly IMapper mapper;

    public ItemControllerAsync(
        IInventoryUnitOfWork uow
        , IMapper mapper)
    {
        this.uow = uow;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ItemReadDto>>> GetItemsAsync()
    {
        var items = await uow.Item.GetItemsAsync();
		return Ok(mapper.Map<IEnumerable<ItemReadDto>>(items));
    }

    [HttpGet("category/{categoryId:int}")]
    public async Task<ActionResult<IEnumerable<ItemReadDto>>> GetItemsInOneCategoryAsync(int categoryId)
    {
        var items = await uow.Item.GetItemsInOneCategoryAsync(categoryId);
		return Ok(mapper.Map<IEnumerable<ItemReadDto>>(items));
    }

    [HttpGet("state/{stateId:int}")]
    public async Task<ActionResult<IEnumerable<ItemReadDto>>> GetItemsExcludingOneStateAsync(int stateId)
    {
        var items = await uow.Item.GetItemsExcludingOneStateAsync(stateId);
		return Ok(mapper.Map<IEnumerable<ItemReadDto>>(items));
    }

    [HttpGet("relatedExcludingOneState/{parentId:int}/{stateId:int}")]
    public async Task<ActionResult<IEnumerable<ItemReadDto>>> GetRelatedItemsExcludingOneStateAsync(int parentId, int stateId)
    {
        var items = await uow.Item.GetRelatedItemsExcludingOneStateAsync(parentId, stateId);
        return Ok(mapper.Map<IEnumerable<ItemReadDto>>(items));
    }

    [HttpGet("rootOfOneCategory/{categoryId:int}")]
    public async Task<ActionResult<IEnumerable<ItemReadDto>>> GetRootOfOneCategoryAsync(int categoryId)
    {
        var items = await uow.Item.GetRootItemsOfOneCategory(categoryId);
        return Ok(mapper.Map<IEnumerable<ItemReadDto>>(items));
    }

    [HttpGet("{id}", Name=nameof(GetItemByIdAsync))]
	public async Task<ActionResult<ItemReadDto>> GetItemByIdAsync(int id)
	{
		var item = await uow.Item.GetByIdAsync(id);
		if(item != null)
		{
			return Ok(mapper.Map<ItemReadDto>(item));
		}
		return NotFound();
	}

	[HttpPost]
	public async Task<ActionResult<ItemReadDto>> CreateItemAsync(ItemCreateDto dto)
	{
		var item = mapper.Map<Item>(dto);
		await uow.Item.InsertAsync(item);
		await uow.SaveAsync();
		var itemReadDto = mapper.Map<ItemReadDto>(item);
		return CreatedAtRoute(nameof(GetItemByIdAsync), new {Id = itemReadDto.Id}, itemReadDto);
	}

	[HttpPut("{id}")]
	public async Task<ActionResult> UpdateItemAsync(
		int id
		, ItemUpdateDto dto)
	{
		var item = await uow.Item.GetByIdAsync(id);
		if(item == null)
		{
			return NotFound();
		}
		mapper.Map(dto, item);
		uow.Item.Update(item);
		await uow.SaveAsync();
		return NoContent();
	}

    [HttpPatch("{id}")]
	public async Task<ActionResult> PartialItemUpdateAsync(
		int id
		, JsonPatchDocument<ItemUpdateDto> patchDoc)
	{
		var item = await uow.Item.GetByIdAsync(id);
		if(item == null)
		{
			return NotFound();
		}
		var itemDto = mapper.Map<ItemUpdateDto>(item);
		patchDoc.ApplyTo(itemDto, ModelState);
		if(TryValidateModel(itemDto) == false)
		{
			return ValidationProblem(ModelState);
		}
		mapper.Map(itemDto, item);
		uow.Item.Update(item);
		await uow.SaveAsync();
		return NoContent();
	}

    [HttpDelete("{id}")]
	public async Task<ActionResult> DeleteItemAsync(int id)
	{
		if(await uow.Item.DeleteAsync(id) == false)
        {
            return NotFound();
        }
		await uow.SaveAsync();
		return NoContent();
	}
}