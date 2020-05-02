using System.Text.Json.Serialization;

namespace DiscordTiktokUploader.Models
{
    public class Stats
    {
        [JsonPropertyName("diggCount")]
        public long DiggCount { get; set; }

        [JsonPropertyName("shareCount")]
        public long ShareCount { get; set; }

        [JsonPropertyName("commentCount")]
        public long CommentCount { get; set; }

        [JsonPropertyName("playCount")]
        public long PlayCount { get; set; }
    }
}