using SebGlow.Service.Model;
using Newtonsoft.Json;
using System;

namespace SebGlow.Service.Mapping
{
    public class LettersJsonConverter : JsonConverter<Letters>
    {
        public override Letters ReadJson(JsonReader reader, Type objectType, Letters existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, Letters value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            foreach (var occurrence in value.LetterOccurrences)
            {
                writer.WritePropertyName(occurrence.Key.ToString());
                writer.WriteValue(occurrence.Value);
            }
            writer.WriteEndObject();
        }
    }
}
