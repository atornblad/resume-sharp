using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ResumeSharp.Schema
{
    public class Basics
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("label")]
        [Description("e.g. Web Developer")]
        public string Label { get; set; }

        [JsonPropertyName("image")]
        [Description("URL (as per RFC 3986) to a image in JPEG or PNG format")]
        [Url]
        public string Image { get; set; }

        [JsonPropertyName("email")]
        [Description("e.g. thomas@gmail.com")]
        [EmailAddress]
        public string Email { get; set; }

        [JsonPropertyName("phone")]
        [Description("Phone numbers are stored as strings so use any format you like, e.g. 712-117-2923")]
        [Phone]
        public string Phone { get; set; }

        [JsonPropertyName("url")]
        [Description("URL (as per RFC 3986) to your website, e.g. personal homepage")]
        [Url]
        public string Url { get; set; }

        [JsonPropertyName("summary")]
        [Description("Write a short 2-3 sentence biography about yourself")]
        public string Summary { get; set; }

        [JsonPropertyName("location")]
        public Location Location { get; set; }
    }
}
