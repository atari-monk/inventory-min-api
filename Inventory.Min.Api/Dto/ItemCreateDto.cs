using System.ComponentModel.DataAnnotations;
using ModelHelper;

namespace Inventory.Min.Api.Dto;

public class ItemCreateDto
    : Model
{
    [Required]
    [MaxLength(NameMax)]
	public string? Name { get; set; }

    [MaxLength(DescriptionMax)]
	public string? Description { get; set; }
}