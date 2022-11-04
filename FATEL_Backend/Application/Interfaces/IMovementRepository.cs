using Domain;

namespace Application.Interfaces;

public interface IMovementRepository
{
    public Entry Record(Item item, Entry entry);
}