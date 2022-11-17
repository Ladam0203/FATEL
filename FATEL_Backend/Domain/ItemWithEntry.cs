namespace Domain;

public class ItemWithEntry : Item
{
    public ItemWithEntry(Item item, Entry entry)
    {
        Id = item.Id;
        Name = item.Name;
        Entry = entry;
    }
    
    public Entry Entry { get; set; }
}