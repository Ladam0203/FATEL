using Application.Interfaces;
using Domain;

namespace Infrastructure;

public class EntryRepository : IEntryRepository
{

    private readonly AppDbContext _context;
    
    public EntryRepository(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public Entry Create(Entry entry)
    {
        _context.EntryTable.Add(entry);
        _context.SaveChanges();
        return entry;
    }

    public List<Entry> ReadAll()
    {
        return _context.EntryTable.ToList();
    }
}
