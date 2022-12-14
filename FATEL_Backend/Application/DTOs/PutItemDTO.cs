using Domain;

namespace Application.DTOs;

public class PutItemDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public float? Length { get; set; }
    public float? Width { get; set; }
    public Unit Unit { get; set; }
    public string? Note { get; set; }
}