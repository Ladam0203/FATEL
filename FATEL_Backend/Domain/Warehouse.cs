namespace Domain;

public class Warehouse
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Item> Inventory { get; set; } = new List<Item>();
    public ICollection<Entry> Diary { get; set;} = new List<Entry>();
}