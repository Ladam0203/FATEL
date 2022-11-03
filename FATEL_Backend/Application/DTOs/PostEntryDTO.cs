namespace Application.DTOs;

public class PostEntryDTO
{
    public DateTime Timestamp { get; set; }
    public int ItemId { get; set; }
    public string ItemName { get; set; }
    public double Change { get; set; }
    public double QuantityAfterChange { get; set; }
}