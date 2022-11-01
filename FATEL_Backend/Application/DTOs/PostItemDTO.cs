using Domain;

namespace Application.DTOs;

public class PostItemDTO
{

    public string Name { get; set; }
    public float?  Width { get; set; }
    public float? Length { get; set; }
    public Unit Unit { get; set; }
    public int Quantity { get; set; }
    public string? Note { get; set; }
}