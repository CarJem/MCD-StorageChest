using DungeonTools.Save.File;
using MCDStorageChest.Json.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using FModel;
using PakReader;
using PakReader.Pak;
using PakReader.Parsers.Objects;
using SkiaSharp;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using MCDStorageChest;
using MCDStorageChest.Json;
using MCDStorageChest.Logic;
using MCDStorageChest.Logic.ImageResolver;
using System.Windows;
#nullable enable

namespace MCDStorageChest.Logic
{
    public static class AssetLoader
    {
        public static IImageResolver instance = new LocalImageResolver();

        public static bool gameContentLoaded { get; private set; } = false;

        static AssetLoader()
        {
            Globals.Game = new FGame(EGame.MinecraftDungeons, EPakVersion.FNAME_BASED_COMPRESSION_METHOD);
        }

        public static async Task FileLoadGameContent(string paksFolderPath)
        {
            try
            {
                Secrets.LoadAESKeys();
                var pakIndex = await loadPakIndex(paksFolderPath);
                if (pakIndex == null)
                {
                    throw new NullReferenceException($"PakIndex is null. Cannot Continue.");
                }
                var pakImageResolver = new PakImageResolver(pakIndex!, paksFolderPath);
                await pakImageResolver.loadPakFilesAsync(preloadBitmaps: false);
                instance = pakImageResolver;
                gameContentLoaded = true;
                Json.Mapping.ImageMappings.Instance.UpdateProperties();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                FileUnloadGameContent();
            }

        }
        public static void FileUnloadGameContent()
        {
            if (gameContentLoaded)
            {
                gameContentLoaded = false;
                instance = new LocalImageResolver();
            }
        }

        private static Task<PakIndex?> loadPakIndex(string paksFolderPath)
        {
            var tcs = new TaskCompletionSource<PakIndex?>();
            Task.Run(() =>
            {
                try
                {
                    var filter = new PakFilter(new[] { Constants.PAKS_FILTER_STRING }, false);
                    var pakIndex = new PakIndex(path: paksFolderPath, cacheFiles: true, caseSensitive: true, filter: filter);
                    if (pakIndex.PakFileCount == 0)
                    {
                        throw new FileNotFoundException($"No files were found at {paksFolderPath}");
                    }
                    if (!unlockPakIndex(pakIndex))
                    {
                        throw new InvalidOperationException($"Could not decrypt pak files at {paksFolderPath}");
                    }
                    tcs.SetResult(pakIndex);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Could not load Minecraft Dungeons Paks: {e}");
                    tcs.SetException(e);
                }
            });
            return tcs.Task;
        }
        private static bool unlockPakIndex(PakIndex pakIndex)
        {
            foreach (var keyStr in Secrets.PAKS_AES_KEY_STRINGS)
            {
                byte[] keyBytes;
                if (keyStr.StartsWith("0x"))
                {
                    keyBytes = keyStr.Substring(2).ToBytesKey();
                }
                else
                {
                    keyBytes = keyStr.ToBytesKey();
                }
                var count = pakIndex.UseKey(FGuid.Zero, keyBytes);
                if (count > 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
