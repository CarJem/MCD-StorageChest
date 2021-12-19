using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCDStorageChest
{
    public static class Secrets
    {
        // Fill in the value for this one
        public static string[] PAKS_AES_KEY_STRINGS = new string[] {
            "0xCCB6409075433E2BE4CC249C48D9B5350741A9F9399A51B9E3B6742EF13947FD",
            "0xC4D2F53C2244E99D44CB125F2885A3007843492EB8814704D08A6F8D748E3055",
            "0x7CCEE565351B5B329889F06CA16F880117894EA6FA9549CFB4879B9640FF5380",
            "0x72FA28089D1E511139E4EA804A6577B37070940DC43D72D62D2C889A728D9620"
        };

        // You can leave these empty, they just need to exist
        public const string GAME_ANALYTICS_GAME_KEY = "";
        public const string GAME_ANALYTICS_SECRET_KEY = "";
    }
}
