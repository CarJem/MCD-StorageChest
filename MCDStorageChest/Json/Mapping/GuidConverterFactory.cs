﻿using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MCDStorageChest.Json.Mapping
{
    public class GuidConverterFactory : JsonConverterFactory
    {
        /// <inheritdoc />
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert == typeof(Guid);
        }

        /// <inheritdoc />
        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            return new GuidJsonConverter();
        }
    }
}
