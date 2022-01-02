using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCDStorageChest
{
    public static class Secrets
    {
        public static string[] PAKS_AES_KEY_STRINGS = new string[] { };

        // You can leave these empty, they just need to exist
        public const string GAME_ANALYTICS_GAME_KEY = "";
        public const string GAME_ANALYTICS_SECRET_KEY = "";

        public static void LoadAESKeys()
        {
            string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "AES-Keys.txt");
            if (File.Exists(fileName))
            {
                try
                {
                    List<string> AESKeys = File.ReadAllLines(fileName).ToList();
                    PAKS_AES_KEY_STRINGS = AESKeys.ToArray();
                }
                catch
                {
                   
                }
            }

        }
    }
}
