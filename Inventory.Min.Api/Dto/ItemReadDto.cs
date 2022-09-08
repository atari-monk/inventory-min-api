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

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? SellPrice { get; set; }

    [MaxLength(PathMax)]
	public string? ImagePath { get; set; }

    [ForeignKey(nameof(Unit))]
	public int? LengthUnitId { get; set; }

    public double? Length { get; set; }

	public double? Heigth { get; set; }

	public double? Depth { get; set; }

    public double?  Diameter { get; set; }

    [ForeignKey(nameof(Unit))]
	public int? VolumeUnitId { get; set; }

    public double? Volume { get; set; }

    public double? Mass { get; set; }

    [ForeignKey(nameof(Unit))]
	public int? MassUnitId { get; set; }

    [ForeignKey(nameof(Tag))]
	public int? TagId { get; set; }

    [ForeignKey(nameof(State))]
	public int? StateId { get; set; }

    [ForeignKey(nameof(Item))]
	public int? ParentId { get; set; }
}