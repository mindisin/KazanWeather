using System.Text.Json.Serialization;

namespace ServiceB.Models
{
    public class WeatherModel
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("temp")]
        public float Temperature { get; set; }

        [JsonPropertyName("feels_like")]
        public float TemperatureFeelsLike { get; set; }

        [JsonPropertyName("speed")]
        public float WindSpeed { get; set; }
    }
}
