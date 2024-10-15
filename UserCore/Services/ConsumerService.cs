using Confluent.Kafka;

namespace UserCore.Services;

public class ConsumerService(ILogger<ConsumerService> logger) : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092",
            GroupId = "test",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
        
        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        consumer.Subscribe("test");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var consumerResult = consumer.Consume(TimeSpan.FromSeconds(5));
                if(consumerResult is null) 
                    continue;
                logger.LogInformation($"Получили сообщение '{consumerResult.Message.Value}' " +
                                      $"со смещением '{consumerResult.Offset}'");
            }
            catch (OperationCanceledException ex)
            {
                logger.LogError($"Ошибка: {ex.Message}");
            }    
        }
        
        return Task.CompletedTask;
    }
}