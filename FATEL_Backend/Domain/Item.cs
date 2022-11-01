

namespace Domain;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
    public float? Length { get; set; }
    public float? Width { get; set; }
    public Unit Unit { get; set; }
    public int Quantity { get; set; }
    public string? Note { get; set; }
}