using Domain;

namespace Application.Interfaces;

public interface IItemRepository
{
    public Item Create(Item item);
    public Item Read(int id);
    public List<Item> ReadAll();
    public Item Update(Item item);
    public Item Delete(int id);
    public double ReadTotalQuantityOf(string itemName);
    public bool DoesIdenticalExist(Item item);
}