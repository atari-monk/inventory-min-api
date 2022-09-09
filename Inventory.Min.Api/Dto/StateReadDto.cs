using System.ComponentModel.DataAnnotations;
using ModelHelper;

namespace Inventory.Min.Api.Dto;

public class StateReadDto
    : Model
{
	[Required]
    [Range(IdMin, IdMax, ErrorMessage = IdError)]
	public int Id { get; set; }

	[Required]
    [MaxLength(NameMax)]
	public string? Name { get; set; }

    [MaxLength(DescriptionMax)]
	public string? Description { get; set; }
}