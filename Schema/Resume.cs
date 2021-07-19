using System.Text.Json.Serialization;

namespace ResumeSharp.Schema
{
    public class Resume
    {
        [JsonPropertyName("basics")]
        public Basics Basics { get; set; }
    }
}
