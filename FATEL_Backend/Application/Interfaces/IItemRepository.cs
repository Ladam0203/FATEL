using Domain;

namespace Application.Interfaces;

public interface IItemRepository
{
    Item Create(Item item);
    Item Read(int id);
    List<Item> ReadAll();
    Item Update(Item item);

    List<Item> UpdateRange(List<Item> items);
    Item Delete(int id);
    double ReadTotalQuantityOf(Item item);
    bool DoesIdenticalExist(Item item);
}