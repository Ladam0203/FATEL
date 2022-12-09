using Domain;

namespace Application.Interfaces;

public interface IEntryService
{
    List<Entry> ReadAll();
}