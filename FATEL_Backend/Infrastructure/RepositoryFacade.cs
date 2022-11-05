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
    
    public Item CreateAndRecord(Item item, Entry entry)
    {
        using (var dbContextTransaction = _context.Database.BeginTransaction())
        {
            var newItem = _itemRepository.Create(item);
            
            entry.Id = newItem.Id;
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
    
    public double ReadTotalQuantityOf(string itemName)
    {
        return _itemRepository.ReadTotalQuantityOf(itemName);
    }
}