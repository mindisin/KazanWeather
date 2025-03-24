using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ServiceC.Domain
{
    public class Weather
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime WeatherDate { get; set; }

        /// <summary>
        /// Скорость ветра в метрах в секунду
        /// </summary>
        [JsonPropertyName("windSpeed")]
        public float WindSpeed { get; set; }

        /// <summary>
        /// Температура в цельсиях
        /// </summary>
        [JsonPropertyName("temperature")]
        public float Temperature { get; set; }

        [JsonPropertyName("feelsLike")]
        public float TemperatureFeelsLike { get; set; }

        [JsonPropertyName("description")]
        public WeatherType Description { get; set; }
    }
}