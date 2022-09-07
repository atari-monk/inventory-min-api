using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Inventory.Min.Data;
using ModelHelper;

namespace Inventory.Min.Api.Dto;

public class ItemReadDto
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

    public int? InitialCount { get; set; }

    public int? CurrentCount { get; set; }

    [ForeignKey(nameof(Category))]
	public int? CategoryId { get; set; }

	[Column(TypeName = Datetime2Name)]
	public DateTime? PurchaseDate { get; set; }

    [ForeignKey(nameof(Currency))]
	public int? CurrencyId { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? PurchasePrice { get; set; }
}