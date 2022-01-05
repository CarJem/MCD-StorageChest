using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCDStorageChest.Json
{
    public class DynamicJSON
    {
        [JsonExtensionData]
        public Dictionary<string, JToken> Data { get; set; } = new Dictionary<string, JToken>();
    }
    
}
