namespace Domain;

public class Warehouse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Item> Inventory { get; set; }
    public ICollection<Entry> Diary { get; set;}
}