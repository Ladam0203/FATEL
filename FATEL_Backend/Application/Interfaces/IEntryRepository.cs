using Domain;

namespace Application.Interfaces;

public interface IEntryRepository
{
    Entry Create(Entry entry);
    List<Entry> ReadAll();
}
