using Domain;

namespace Application.Interfaces;

public interface IItemRepository
{
    Item Create(Item item);
    Item Read(int id);
    List<Item> ReadAll();
    Item Update(Item item);
    Item UpdateQuantity(Item item);
    List<Item> UpdateNameRange(List<Item> items);
    Item Delete(int id);
    double ReadTotalQuantityOf(Item item);
    bool DoesIdenticalExist(Item item);
}