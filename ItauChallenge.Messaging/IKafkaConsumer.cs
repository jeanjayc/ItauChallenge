namespace ItauChallenge.Messaging;

public interface IKafkaConsumer
{
    Task ConsumeAsync(
        string topic,
        Func<string, CancellationToken, Task> messageHandler,
        CancellationToken cancellationToken = default);
}
