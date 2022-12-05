using Application.DTOs;
using Domain;

namespace Application.Interfaces;

public interface IRepositoryFacade
{
    Item ReadItem(int id);
    List<Item> ReadAllItems();
    double ReadTotalQuantityOf(Item item);
    Item CreateItem(Item item);
    Item CreateAndRecord(Item item, Entry entry);
    Item UpdateQuantityAndRecord(Item item, Entry entry);
    Item UpdateItem(Item item);
    List<Item> UpdateItemRange(List<Item> items);
    Item DeleteItem(int id);
    bool DoesIdenticalItemExist(Item item);
    List<Entry> ReadAllEntries();
    Item DeleteAndRecord(int id, Entry entry);
    Warehouse CreateWarehouse(Warehouse warehouse);
    List<Warehouse> ReadAllWarehouses();
    //Read???
    Warehouse UpdateWarehouse(Warehouse warehouse);
    Warehouse DeleteWarehouse(int id);
}