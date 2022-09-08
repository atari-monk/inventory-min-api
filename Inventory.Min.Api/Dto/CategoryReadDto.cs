using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Inventory.Min.Data;
using ModelHelper;

namespace Inventory.Min.Api.Dto;

public class CategoryReadDto
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

    [ForeignKey(nameof(Category))]
	public int? ParentId { get; set; }
}