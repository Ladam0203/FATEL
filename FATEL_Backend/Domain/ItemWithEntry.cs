namespace Domain;

public class ItemWithEntry : Item
{
    public ItemWithEntry(Item item, Entry entry)
    {
        Id = item.Id;
        Name = item.Name;
        Length = item.Length;
        Width = item.Width;
        Unit = item.Unit;
        Quantity = item.Quantity;
        Note = item.Note;
        Entry = entry;
    }
    
    public Entry Entry { get; set; }
}