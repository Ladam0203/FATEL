using Application.Interfaces;
using Domain;
using Infrastructure;

namespace Test;

public class RepositoryFacade : IRepositoryFacade
{
    private readonly AppDbContext _context;
    private readonly IItemRepository _itemRepository;
    private readonly IEntryRepository _entryRepository;

    public RepositoryFacade(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        
        _itemRepository = new ItemRepository(context);
        _entryRepository = new EntryRepository(context);
    }
    
    public Item UpdateQuantityAndRecord(Item item, Entry entry)
    {
        using (var dbContextTransaction = _context.Database.BeginTransaction())
        {
            _itemRepository.Update(item);
            
            _entryRepository.Create(entry);
                
            _context.SaveChanges();

            dbContextTransaction.Commit();
        }

        return item;
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
            _entryRepository.Create(entry);
                
            _context.SaveChanges();

            dbContextTransaction.Commit();
        }
        return item;
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
        Item item;
        
        using (var dbContextTransaction = _context.Database.BeginTransaction())
        {
           item = _itemRepository.Delete(id);
            _entryRepository.Create(entry);
                
            _context.SaveChanges();

            dbContextTransaction.Commit();
        }
        return item;
    }
}