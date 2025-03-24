using Newtonsoft.Json;

namespace ServiceA.Models
{
    public class Weather
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("temp")]
        public float Temperature { get; set; }

        [JsonProperty("feels_like")]
        public float TemperatureFeelsLike { get; set; }

        [JsonProperty("speed")]
        public float WindSpeed { get; set; }
    }
}