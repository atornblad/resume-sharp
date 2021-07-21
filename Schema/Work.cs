using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ResumeSharp.Schema
{
    public class Work
    {
        [JsonPropertyName("name")]
        [Description("e.g. Facebook")]
        public string Name { get; set; }

        [JsonPropertyName("location")]
        [Description("e.g. Menlo Park, CA")]
        public string Location { get; set; }

        [JsonPropertyName("description")]
        [Description("e.g. Social Media Company")]
        public string Description { get; set; }

        [JsonPropertyName("position")]
        [Description("e.g. Software Engineer")]
        public string Position { get; set; }

        [JsonPropertyName("url")]
        [Description("e.g. http://facebook.example.com")]
        [Url]
        public string Url { get; set; }

        [JsonPropertyName("startDate")]
        public Date StartDate { get; set; }

        [JsonPropertyName("endDate")]
        public Date EndDate { get; set; }

        [JsonPropertyName("summary")]
        [Description("Give an overview of your responsibilities at the company")]
        public string Summary { get; set; }

        [JsonPropertyName("highlights")]
        [Description("Specify multiple accomplishments: e.g. Increased profits by 20% from 2011-2012 through viral advertising")]
        public string[] Highlights { get; set; }
    }
}