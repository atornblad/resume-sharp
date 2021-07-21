using System.Text.Json.Serialization;

// Source: https://github.com/jsonresume/resume-schema/blob/master/schema.json

namespace ResumeSharp.Schema
{
    public class Resume
    {
        [JsonPropertyName("basics")]
        public Basics Basics { get; set; }
    }
}
