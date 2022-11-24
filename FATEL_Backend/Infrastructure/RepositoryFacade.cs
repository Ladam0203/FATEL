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
        
        //rebuild db
        //_context.Database.EnsureDeleted();
        //_context.Database.EnsureCreated();
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
    
    public double ReadTotalQuantityOf(Item item)
    {
        return _itemRepository.ReadTotalQuantityOf(item);
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
            
            newItem.Warehouse = null;
            newEntry.Warehouse = null;
            return new ItemWithEntry(newItem, newEntry);
        }
    }
    

    public Warehouse CreateWarehouse(Warehouse warehouse)
    {
        return _warehouseRepository.Create(warehouse);
    }

    public List<Warehouse> ReadAllWarehouses()
    {
        return _warehouseRepository.ReadAll();
    }

    public Warehouse UpdateWarehouse(Warehouse warehouse)
    {
        return _warehouseRepository.Update(warehouse);
    }

    public Warehouse DeleteWarehouse(int id)
    {
        return _warehouseRepository.Delete(id);
    }
}