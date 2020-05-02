using System;
using System.Text.Json.Serialization;

namespace DiscordTiktokUploader.Models
{
    public class Music
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("playUrl")]
        public string PlayUrl { get; set; }

        [JsonPropertyName("coverThumb")]
        public string CoverThumb { get; set; }

        [JsonPropertyName("coverMedium")]
        public Uri CoverMedium { get; set; }

        [JsonPropertyName("coverLarge")]
        public Uri CoverLarge { get; set; }

        [JsonPropertyName("authorName")]
        public string AuthorName { get; set; }

        [JsonPropertyName("original")]
        public bool Original { get; set; }
    }
}