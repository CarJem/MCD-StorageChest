using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MCDStorageChest.Save.Mapping;
using MCDStorageChest.Save.Json;
using MCDStorageChest.Save.Json.Mapping;
using MCDStorageChest.Save.Profiles;
using Newtonsoft.Json.Linq;

namespace MCDStorageChest.Save.Json
{
    public static class ProfileParser
    {
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions();

        static ProfileParser()
        {
            _options.Converters.Add(new AttributeBasedConverterFactory());
            _options.Converters.Add(new GuidConverterFactory());
            _options.Converters.Add(new JsonStringEnumConverter());
            //_options.Converters.Add(new TextDoubleJsonConverter());
            _options.IgnoreNullValues = true;
            _options.WriteIndented = true;
        }

        public static async ValueTask<ProfileSaveFile> Read(Stream stream)
        {
            return await JsonSerializer.DeserializeAsync<ProfileSaveFile>(stream, _options);
        }

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
            Stream stream = new MemoryStream();
            await JsonSerializer.SerializeAsync(stream, settings, _options);
            return stream;
        }

    }
}
