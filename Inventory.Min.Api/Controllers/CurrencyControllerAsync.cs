using AutoMapper;
using Inventory.Min.Api.Dto;
using Inventory.Min.Data;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Min.Api.Controllers;

[ApiController]
[Route("api/currencies")]
public class CurrencyControllerAsync
    : ControllerBase
{
    private readonly IInventoryUnitOfWork uow;
    private readonly IMapper mapper;

    public CurrencyControllerAsync(
        IInventoryUnitOfWork uow
        , IMapper mapper)
    {
        this.uow = uow;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CurrencyReadDto>>> GetAsync()
    {
        var items = await uow.Currency.GetAsync();
		return Ok(mapper.Map<IEnumerable<CurrencyReadDto>>(items));
    }
}