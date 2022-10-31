namespace Domain;

public enum Unit
{
    Meter,
    SquareMeter,
    Piece
}

public class Model
{
    public Unit Unit { get; set; }
}