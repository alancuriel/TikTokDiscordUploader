using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DiscordTiktokUploader.Models
{
    public class Item
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("desc")]
        public string Desc { get; set; }

        [JsonPropertyName("createTime")]
        public long CreateTime { get; set; }

        [JsonPropertyName("video")]
        public Video Video { get; set; }

        [JsonPropertyName("author")]
        public Author Author { get; set; }

        [JsonPropertyName("music")]
        public Music Music { get; set; }

        [JsonPropertyName("stats")]
        public Stats Stats { get; set; }

        [JsonPropertyName("originalItem")]
        public bool OriginalItem { get; set; }

        [JsonPropertyName("officalItem")]
        public bool OfficalItem { get; set; }

        [JsonPropertyName("secret")]
        public bool Secret { get; set; }

        [JsonPropertyName("forFriend")]
        public bool ForFriend { get; set; }

        [JsonPropertyName("digged")]
        public bool Digged { get; set; }

        [JsonPropertyName("itemCommentStatus")]
        public long ItemCommentStatus { get; set; }

        [JsonPropertyName("showNotPass")]
        public bool ShowNotPass { get; set; }

        [JsonPropertyName("vl1")]
        public bool Vl1 { get; set; }

        
    }

}
