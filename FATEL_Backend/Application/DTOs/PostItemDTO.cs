using Domain;

namespace Application.DTOs;

public class PostItemDTO
{
    public string Name { get; set; }
    public float? Length { get; set; }
    public float?  Width { get; set; }
    public Unit Unit { get; set; }
    public int Quantity { get; set; } //TODO: If we create an item with initial quantity a diary entry has to be made later on
    public string? Note { get; set; }
    public  int WarehouseId { get; set; }
}