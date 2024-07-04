using System.Text.Json.Serialization;

namespace stream_api_challenge.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StreamingEnum
    {
        Netflix,
        AmazonPrime,
        DisneyPlus,
        DiscoveryPlus,
        HBO,
        AppleTV,
        GooglePlay,
        MercadoPlay
    }
}
