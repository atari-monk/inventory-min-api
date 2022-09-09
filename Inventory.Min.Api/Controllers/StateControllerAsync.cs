using AutoMapper;
using Inventory.Min.Api.Dto;
using Inventory.Min.Data;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Min.Api.Controllers;

[ApiController]
[Route("api/states")]
public class StateControllerAsync
    : ControllerBase
{
    private readonly IInventoryUnitOfWork uow;
    private readonly IMapper mapper;

    public StateControllerAsync(
        IInventoryUnitOfWork uow
        , IMapper mapper)
    {
        this.uow = uow;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<StateReadDto>>> GetAsync()
    {
        var items = await uow.State.GetAsync();
		return Ok(mapper.Map<IEnumerable<StateReadDto>>(items));
    }
}