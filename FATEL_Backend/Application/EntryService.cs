using Application.Interfaces;
using Domain;

namespace Application;

public class EntryService : IEntryService
{

    private readonly IRepositoryFacade _repository;

    public EntryService(IRepositoryFacade repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }
    public List<Entry> ReadAll()
    {
        return _repository.ReadAllEntries();
    }
}