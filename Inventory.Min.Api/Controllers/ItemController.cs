using AutoMapper;
using Inventory.Min.Api.Dto;
using Inventory.Min.Data;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Min.Api.Controllers;

[ApiController]
[Route("api/items")]
public class ItemController
    : ControllerBase
{
    private readonly IInventoryUnitOfWork uow;
    private readonly IMapper mapper;

    public ItemController(
        IInventoryUnitOfWork uow
        , IMapper mapper)
    {
        this.uow = uow;
        this.mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ItemReadDto>> Get()
    {
        var items = uow.Item.Get();
		return Ok(mapper.Map<IEnumerable<ItemReadDto>>(items));
    }

    [HttpGet("{id}", Name=nameof(GetItemById))]
	public ActionResult<ItemReadDto> GetItemById(int id)
	{
		var item = uow.Item.GetByID(id);
		if(item != null)
		{
			return Ok(mapper.Map<ItemReadDto>(item));
		}
		return NotFound();
	}

	[HttpPost]
	public ActionResult<ItemReadDto> CreateItem(ItemCreateDto dto)
	{
		var item = mapper.Map<Item>(dto);
		uow.Item.Insert(item);
		uow.Save();

		var itemReadDto = mapper.Map<ItemReadDto>(item);

		return CreatedAtRoute(nameof(GetItemById), new {Id = itemReadDto.Id}, itemReadDto);
	}

	[HttpPut("{id}")]
	public ActionResult UpdateItem(
		int id
		, ItemUpdateDto dto)
	{
		var item = uow.Item.GetByID(id);
		if(item == null)
		{
			return NotFound();
		}
		mapper.Map(dto, item);
		
		uow.Item.Update(item);

		uow.Save();

		return NoContent();
	}

    [HttpPatch("{id}")]
	public ActionResult PartialItemUpdate(
		int id
		, JsonPatchDocument<ItemUpdateDto> patchDoc)
	{
		var item = uow.Item.GetByID(id);
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
		uow.Save();
		return NoContent();
	}
}