using AutoMapper;
using Inventory.Min.Api.Dto;
using Inventory.Min.Data;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Min.Api.Controllers;

[ApiController]
[Route("api/units")]
public class UnitControllerAsync
    : ControllerBase
{
    private readonly IInventoryUnitOfWork uow;
    private readonly IMapper mapper;

    public UnitControllerAsync(
        IInventoryUnitOfWork uow
        , IMapper mapper)
    {
        this.uow = uow;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UnitReadDto>>> GetAsync()
    {
        var items = await uow.Unit.GetAsync();
		return Ok(mapper.Map<IEnumerable<UnitReadDto>>(items));
    }
}