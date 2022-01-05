using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using MCDStorageChest.Json.Mapping;
using MCDStorageChest.Json;
using MCDStorageChest.Json.Classes;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using System;
using System.Globalization;
using System.Linq;
#nullable enable

namespace MCDStorageChest.Json
{
    public class GuidJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert == typeof(Guid);
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            return Guid.Parse(reader.Value?.ToString());
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value != null && value.GetType() == typeof(Guid))
            {
                var guid = (Guid)value;
                writer.WriteValue(guid.ToString("N").ToUpper());
            }

        }
    }

    public class DecimalJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(decimal) || objectType == typeof(decimal?));
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            if (token.Type == JTokenType.Float || token.Type == JTokenType.Integer)
            {
                return token.ToObject<decimal>();
            }
            if (token.Type == JTokenType.String)
            {
                // customize this to suit your needs
                return Decimal.Parse(token.ToString(),
                       System.Globalization.CultureInfo.GetCultureInfo("es-ES"));
            }
            if (token.Type == JTokenType.Null && objectType == typeof(decimal?))
            {
                return null;
            }
            throw new JsonSerializationException("Unexpected token type: " +
                                                  token.Type.ToString());
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            writer.WriteValue(value);
        }
    }



    public static class ProfileParser
    {
        private static readonly JsonSerializerSettings _options = new JsonSerializerSettings();

        static ProfileParser()
        {
            _options.Converters.Add(new GuidJsonConverter());
            _options.Converters.Add(new StringEnumConverter());
            _options.Converters.Add(new DecimalJsonConverter());
            _options.NullValueHandling = NullValueHandling.Ignore;
            _options.FloatParseHandling = FloatParseHandling.Decimal;
            _options.Formatting = Formatting.Indented;
        }

        #pragma warning disable CS8603 // Possible null reference return.
        public static async ValueTask<ProfileSaveFile> Read(Stream stream)
        {
            return await Task.Run(() =>
            {
                using (var sr = new StreamReader(stream))
                    using (var json = new JsonTextReader(sr))
                        return JsonSerializer.Create(_options).Deserialize<ProfileSaveFile>(json);
        });

        }
        #pragma warning restore CS8603 // Possible null reference return.

        private static MemoryStream Sanitize(Stream stream)
        {
            using BinaryReader reader = new BinaryReader(stream, Encoding.UTF8, false);
            using MemoryStream sanitized = new MemoryStream();

            string text = string.Empty;

            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                byte b = reader.ReadByte();
                if (char.IsControl((char)b))
                {
                    continue;
                }

                text += (char)b;
                sanitized.WriteByte(b);
            }

            sanitized.Seek(0, SeekOrigin.Begin);
            return sanitized;
        }

        public static async ValueTask<Stream> Write(ProfileSaveFile settings)
        {
            return await Task.Run(() =>
            {
                var stream = new MemoryStream();
                using (var streamWriter = new StreamWriter(stream: stream, encoding: Encoding.UTF8, bufferSize: 4096, leaveOpen: true)) // last parameter is important
                using (var jsonWriter = new JsonTextWriter(streamWriter))
                {
                    var serializer = JsonSerializer.Create(_options);
                    serializer.Serialize(jsonWriter, settings);
                    streamWriter.Flush();
                    stream.Seek(0, SeekOrigin.Begin);
                    return stream;
                }
            });
        }

    }
}
