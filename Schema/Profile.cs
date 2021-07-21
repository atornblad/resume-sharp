using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ResumeSharp.Schema
{
    public class Profile
    {
        [JsonPropertyName("network")]
        [Description("e.g. Facebook or Twitter")]
        public string Network { get; set; }

        [JsonPropertyName("username")]
        [Description("e.g. neutralthoughts")]
        public string Username { get; set; }

        [JsonPropertyName("url")]
        [Description("e.g. http://twitter.example.com/neutralthoughts")]
        [Url]
        public string Url { get; set; }
    }
}