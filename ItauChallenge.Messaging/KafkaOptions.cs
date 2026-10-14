namespace ItauChallenge.Messaging;

public class KafkaOptions
{
    public string BootstrapServers { get; set; } = string.Empty;
    public string Topic { get; set; } = "quotes-updates";
    public string ConsumerGroupId { get; set; } = "quotes-consumer";
}
