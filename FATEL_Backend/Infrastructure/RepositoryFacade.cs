using Application.DTOs;
using Application.Interfaces;
using Domain;

namespace Infrastructure;

public class RepositoryFacade : IRepositoryFacade
{
    private readonly AppDbContext _context;
    private readonly IItemRepository _itemRepository;
    private readonly IEntryRepository _entryRepository;
    private readonly IWarehouseRepository _warehouseRepository;

    public RepositoryFacade(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        
        _itemRepository = new ItemRepository(context);
        _entryRepository = new EntryRepository(context);
        _warehouseRepository = new WarehouseRepository(context);
    }
    
    public Item UpdateQuantityAndRecord(Item item, Entry entry)
    {
        using (var dbContextTransaction = _context.Database.BeginTransaction())
        {
            var newItem = _itemRepository.Update(item);
            
            var newEntry = _entryRepository.Create(entry);

            _context.SaveChanges();

            dbContextTransaction.Commit();
            
            return new ItemWithEntry(newItem, newEntry);
        }
    }

    public Item CreateItem(Item item)
    {
        return _itemRepository.Create(item);
    }

    public Item CreateAndRecord(Item item, Entry entry)
    {
        using (var dbContextTransaction = _context.Database.BeginTransaction())
        {
            var newItem = _itemRepository.Create(item);
            
            entry.ItemId = newItem.Id;
            var newEntry = _entryRepository.Create(entry);
                
            _context.SaveChanges();

            dbContextTransaction.Commit();

            return new ItemWithEntry(newItem, newEntry);
        }
    }

    public Item ReadItem(int id)
    {
        return _itemRepository.Read(id);
    }

    public List<Item> ReadAllItems()
    {
        return _itemRepository.ReadAll();
    }
    
    public double ReadTotalQuantityOf(string itemName)
    {
        return _itemRepository.ReadTotalQuantityOf(itemName);
    }

    public Item UpdateItem(Item item)
    {
        return _itemRepository.Update(item);
    }
    public Item DeleteItem(int id)
    {
        return _itemRepository.Delete(id);
    }

    public bool DoesIdenticalItemExist(Item item)
    {
        return _itemRepository.DoesIdenticalExist(item);
    }

    public List<Entry> ReadAllEntries()
    {
        return _entryRepository.ReadAll();
    }

    public Item DeleteAndRecord(int id, Entry entry)
    {
        using (var dbContextTransaction = _context.Database.BeginTransaction())
        {
            var newItem = _itemRepository.Delete(id);
            var newEntry = _entryRepository.Create(entry);
                
            _context.SaveChanges();

            dbContextTransaction.Commit();
            
            return new ItemWithEntry(newItem, newEntry);
        }
    }

    public Warehouse CreateWarehouse(PostWarehouseDTO dto)
    {
        throw new NotImplementedException();
    }

    public List<Warehouse> ReadAllWarehouses()
    {
        throw new NotImplementedException();
    }

    public Warehouse UpdateWarehouse(PutWarehouseDTO dto)
    {
        throw new NotImplementedException();
    }

    public Warehouse DeleteWarehouse(int id)
    {
        throw new NotImplementedException();
    }
}