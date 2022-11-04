using Domain;

namespace Application.Interfaces;

public interface IMovementRepository
{
    public Entry Record(Movement movement);
}