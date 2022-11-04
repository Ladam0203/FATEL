using Application;
using Application.Interfaces;
using Domain;

namespace Infrastructure;

public class MovementRepository : IMovementRepository
{
    private readonly AppDbContext _context;
    
    public MovementRepository(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public Entry Record(Item item, Entry entry)
    {
        using (var dbContextTransaction = _context.Database.BeginTransaction())
        {
            _context.ItemTable.Update(item);
            _context.EntryTable.Add(entry);
                
            _context.SaveChanges();

            dbContextTransaction.Commit();
        }

        return entry;
    }
}