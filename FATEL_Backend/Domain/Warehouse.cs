namespace Domain;

public class Warehouse
{
    int Id { get; set; }
    string Name { get; set; }
    List<Item> Inventory { get; set; }
    List<Entry> Diary { get; set;}
}