using Confluent.Kafka;

namespace ServiceB.Services
{
    public class WeatherConsumer
    {
        private readonly IConsumer<Null, string> _consumer;
        private readonly ILogger<WeatherConsumer> _logger;

        public WeatherConsumer(ILogger<WeatherConsumer> logger, IConfiguration configuration)
        {
            var bootstrapServers = configuration["KafkaSettings:BootstrapServers"];
            var topicName = configuration["KafkaSettings:TopicName"];
            _logger = logger;

            var config = new ConsumerConfig
            {
                BootstrapServers = bootstrapServers,
                GroupId = "my-consumer-group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<Null, string>(config).Build();
            _consumer.Subscribe(topicName);
        }

        public string? ConsumeMessages(CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var consumeResult = _consumer.Consume(cancellationToken);
                    _logger.LogInformation($"Получено сообщение: {consumeResult.Message.Value}");
                    return consumeResult.Message.Value;
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogError("Остановка Consumer...");
                return null;
            }
            finally
            {
                _consumer.Close();
            }
            return null;
        }
    }

}
