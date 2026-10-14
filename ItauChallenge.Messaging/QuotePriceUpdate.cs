namespace ItauChallenge.Messaging;

public sealed class QuotePriceUpdate
{
    public int QuoteId { get; set; }
    public int AtivoId { get; set; }
    public decimal OldPrice { get; set; }
    public decimal NewPrice { get; set; }
    public DateTimeOffset Timestamp { get; set; }
}
