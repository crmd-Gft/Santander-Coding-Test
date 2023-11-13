using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public class Story
{
    [JsonProperty("title")]
    public string? Title { get; set; }

    [JsonProperty("url")]
    public string? Uri { get; set; }

    [JsonProperty("by")]
    public string? PostedBy { get; set; }

    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime Time { get; set; }

    [JsonProperty("score")]
    public int Score { get; set; }

    [JsonProperty("descendants")]
    public int CommentCount { get; set; }
}
