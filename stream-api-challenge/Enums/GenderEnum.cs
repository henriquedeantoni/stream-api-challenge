using System.Text.Json.Serialization;

namespace stream_api_challenge.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum GenderEnum
    {
        Action,
        Comedy,
        SciFi,
        Mistery,
        Police,
        War,
        Romance,
        Thriller,
        Horror,
        Fantasy,
        Drama,
        Animation,
        Documentary,
        Shows,
        Other
    }
}
