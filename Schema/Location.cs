using System.ComponentModel;
using System.Text.Json.Serialization;

namespace ResumeSharp.Schema
{
    public class Location
    {
        [JsonPropertyName("address")]
        [Description("To add multiple address lines, use \\n. For example, 1234 Glücklichkeit Straße\\nHinterhaus 5. Etage li.")]
        public string Address { get; set; }

        [JsonPropertyName("postalCode")]
        public string PostalCode { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("countryCode")]
        [Description("code as per ISO-3166-1 ALPHA-2, e.g. US, AU, IN")]
        public string CountryCode { get; set; }

        [JsonPropertyName("region")]
        [Description("The general region where you live. Can be a US state, or a province, for instance.")]
        public string Region { get; set; }
    }
}