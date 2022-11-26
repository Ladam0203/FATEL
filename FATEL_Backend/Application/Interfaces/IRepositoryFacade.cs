using Application.DTOs;
using Domain;

namespace Application.Interfaces;

public interface IRepositoryFacade
{
    public Item ReadItem(int id);
    public List<Item> ReadAllItems();
    public double ReadTotalQuantityOf(Item item);
    public Item CreateItem(Item item);
    public Item CreateAndRecord(Item item, Entry entry);
    public Item UpdateQuantityAndRecord(Item item, Entry entry);
    public Item UpdateItem(Item item);
    public Item DeleteItem(int id);
    public bool DoesIdenticalItemExist(Item item);
    List<Entry> ReadAllEntries();
    Item DeleteAndRecord(int id, Entry entry);
    Warehouse CreateWarehouse(Warehouse warehouse);
    List<Warehouse> ReadAllWarehouses();
    //Read???
    Warehouse UpdateWarehouse(Warehouse warehouse);
    Warehouse DeleteWarehouse(int id);
}