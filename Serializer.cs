using System.Text.Json;
using ResumeSharp.Schema;

namespace ResumeSharp
{
    public static class Serializer
    {
        public static Resume Deserialize(string json)
        {
            return JsonSerializer.Deserialize<Resume>(json);
        }
    }
}