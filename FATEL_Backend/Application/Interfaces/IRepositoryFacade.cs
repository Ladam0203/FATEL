using Domain;

namespace Application.Interfaces;

public interface IRepositoryFacade
{
    public Item ReadItem(int id);
    public List<Item> ReadAllItems();
    public double ReadTotalQuantityOf(string itemName);
    public Item CreateAndRecord(Item item, Entry entry);
    public Item UpdateQuantityAndRecord(Item item, Entry entry);
    public Item UpdateItem(Item item);
    public Item DeleteItem(int id);
}