using Domain;

namespace Application.DTOs;

public class PutItemDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public float? Width { get; set; }
    public float? Lenght { get; set; }
    public Unit Unit { get; set; }
    public int Quantity { get; set; }
    public string? Note { get; set; }
}