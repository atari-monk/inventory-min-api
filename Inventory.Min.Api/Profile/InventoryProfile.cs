using AutoMapper;
using Inventory.Min.Api.Dto;
using Inventory.Min.Data;

namespace Inventory.Min.Api;

public class InventoryProfile
    : Profile
{
	public InventoryProfile()
	{
		//Source->Target
		CreateMap<Item, ItemReadDto>();
	}
}