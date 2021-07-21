using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using ResumeSharp.Schema;

namespace ResumeSharp.Formatting
{
    public class Iso8601Converter : JsonConverter<Date>
    {
        public override Date Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return new Date(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, Date value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
