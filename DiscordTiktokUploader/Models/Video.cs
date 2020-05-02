using System.Text.Json.Serialization;

namespace DiscordTiktokUploader.Models
{
    public class Video
    {
        [JsonPropertyName("height")]
        public long Height { get; set; }

        [JsonPropertyName("width")]
        public long Width { get; set; }

        [JsonPropertyName("duration")]
        public long Duration { get; set; }

        [JsonPropertyName("ratio")]
        public string Ratio { get; set; }

        [JsonPropertyName("cover")]
        public string Cover { get; set; }

        [JsonPropertyName("originCover")]
        public string OriginCover { get; set; }

        [JsonPropertyName("dynamicCover")]
        public string DynamicCover { get; set; }

        [JsonPropertyName("playAddr")]
        public string PlayAddr { get; set; }

        [JsonPropertyName("downloadAddr")]
        public string DownloadAddr { get; set; }
    }
}