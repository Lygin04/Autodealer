using Confluent.Kafka;

namespace Autodealer.Services;

public class ProducerService(ILogger<ProducerService> logger)
{
    public async Task ProduceAsync(CancellationToken cancellationToken)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = "localhost:9092",
            AllowAutoCreateTopics = true,
            Acks = Acks.All
        };

        using var producer = new ProducerBuilder<Null, string>(config).Build();

        try
        {
            var deliveryResult = await producer.ProduceAsync(topic: "test",
                new Message<Null, string>
                {
                    Value = $"Hello, Kafka {DateTime.Now}"
                },
                cancellationToken);
            
            logger.LogInformation($"Доставленное сообщение {deliveryResult.Value}," +
                                  $" смещение {deliveryResult.Offset}");
        }
        catch (ProduceException<Null, string> ex)
        {
            logger.LogError($"Неудачная доставка: {ex.Error.Reason}");
        }

        producer.Flush(cancellationToken);
    }
}