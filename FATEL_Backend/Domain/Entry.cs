namespace Domain;

public class Entry
{
    public int Id { get; set; }
    public DateTime Timestamp { get; set; }
    public int ItemId { get; set; }
    public string ItemName { get; set; }
    public double Change { get; set; }
    public double QuantityAfterChange { get; set; }
}