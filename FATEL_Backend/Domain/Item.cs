using Entities;

namespace Domain;

public class Item
{
    public int Id;
    public string Name;
    public float?  Width;
    public float? Lenght;
    public Unit Unit;
    public int Quantity;
    public string? Note;
}