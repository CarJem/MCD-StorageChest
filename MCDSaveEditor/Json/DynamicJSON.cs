using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MCDStorageChest.Json
{
    public class DynamicJSON
    {
        [JsonExtensionData]
        public Dictionary<string, JsonElement> Data { get; set; } = new Dictionary<string, JsonElement>();
    }
    
}
