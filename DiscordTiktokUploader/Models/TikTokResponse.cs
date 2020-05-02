using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DiscordTiktokUploader.Models
{
    public class TikTokResponse
    {
        [JsonPropertyName("statusCode")]
        public long StatusCode { get; set; }

        [JsonPropertyName("items")]
        public Item[] Items { get; set; }

        [JsonPropertyName("hasMore")]
        public bool HasMore { get; set; }

        [JsonPropertyName("maxCursor")]
        public string MaxCursor { get; set; }

        [JsonPropertyName("minCursor")]
        public string MinCursor { get; set; }
    }
}
