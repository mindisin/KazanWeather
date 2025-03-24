using Confluent.Kafka;

namespace ServiceA.Services;

public class WeatherProducer
{
    private readonly string? _topicName;
    private readonly IProducer<Null, string> _producer;
    private readonly ILogger<WeatherProducer> _logger;

    public WeatherProducer(IConfiguration configuration, ILogger<WeatherProducer> logger)
    {
        var bootstrapServers = configuration["KafkaSettings:BootstrapServers"];
        _topicName = configuration["KafkaSettings:TopicName"];
        _logger = logger;

        var config = new ProducerConfig
        {
            BootstrapServers = bootstrapServers
        };

        _producer = new ProducerBuilder<Null, string>(config).Build();
    }

    public async Task ProduceAsync(string message, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation($"Отправка сообщения в топик: {message}");
            await _producer.ProduceAsync(_topicName, new Message<Null, string> { Value = message }, cancellationToken);
            _logger.LogInformation("Сообщение успешно отправлено в топик");
        }
        catch (ProduceException<Null, string> ex)
        {
            _logger.LogError(ex, ex.Message);
        }
    }
}
