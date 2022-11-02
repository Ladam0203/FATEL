using Domain;

namespace Application.DTOs;

public class PostItemDTO
{
    public string Name { get; set; }
    public float? Length { get; set; }
    public float?  Width { get; set; }
    public Unit Unit { get; set; }
    public int Quantity { get; set; } //TODO: This will have to be removed as soon as we have a diary/movement manager
    public string? Note { get; set; }
}