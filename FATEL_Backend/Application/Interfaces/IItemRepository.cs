using Domain;

namespace Application.Interfaces;

public interface IItemRepository
{
    public Item Create(Item item);
    public Item Read(int id);
    public List<Item> ReadAll();
    public Item Update(Item item);
    public Item Delete(string id);
}