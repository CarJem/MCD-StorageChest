using DungeonTools.Save.File;
using System;
using System.IO;
using System.Threading.Tasks;
using MCDStorageChest.Extensions;
using System.Diagnostics;
using MCDStorageChest.Json.Classes;
using MCDStorageChest.Json;
#nullable enable

namespace MCDStorageChest.Logic
{
    public static class FileLoader
    {
        public static async ValueTask<Stream> Decrypt(Stream data)
        {
            Stream decrypted = await AesEncryptionProvider.DecryptAsync(data);
            Stream result = SaveFileHandler.RemoveTrailingZeroes(decrypted);
            result.Seek(0, SeekOrigin.Begin);
            return result;
        }
        public static async ValueTask<Stream> Encrypt(Stream data)
        {
            using Stream encrypted = await AesEncryptionProvider.EncryptAsync(data);
            Stream result = SaveFileHandler.PrependMagicToEncrypted(encrypted);
            result.Seek(0, SeekOrigin.Begin);
            return result;
        }

        #region Load

        public static async Task<ProfileSaveFile?> FileOpenAsyncV2(string filePath)
        {
            Console.WriteLine("Reading file: {0}", filePath);

            var file = new FileInfo(filePath);
            using FileStream inputStream = file.OpenRead();
            bool encrypted = SaveFileHandler.IsFileEncrypted(inputStream);
            if (!encrypted)
                return await TryParseFileStreamAsync(inputStream);
            using Stream processed = await FileLoader.Decrypt(inputStream);
            if (processed == null)
            {
                Debug.WriteLine($"Content of file \"{file.Name}\" could not be converted to a supported format.");
                Debug.WriteLine("R.formatFILE_DECRYPT_ERROR_MESSAGE(file.Name)");
                return null;
            }
            return await TryParseFileStreamAsync(processed);
        }
        private static async Task<ProfileSaveFile?> TryParseFileStreamAsync(Stream stream)
        {
            try
            {
                stream.Seek(0, SeekOrigin.Begin);
                var profile = await ProfileParser.Read(stream);
                if (!profile.isValid())
                {
                    Debug.WriteLine("R.CHARACTER_FILE_FORMAT_NOT_RECOGNIZED_ERROR_MESSAGE");
                    return null;
                }
                return profile;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                Debug.WriteLine("R.FAILED_TO_PARSE_FILE_ERROR_MESSAGE");
            }
            return null;
        }

        #endregion

        #region Save

        public static async Task FileSaveAsyncV2(string filePath, ProfileSaveFile profile)
        {
            bool encrypted = Path.GetExtension(filePath) == Constants.ENCRYPTED_FILE_EXTENSION;

            if (filePath == null) { return; }
            profile.TotalGearPower = profile.computeCharacterPower();
            Console.WriteLine("Writing file: {0}", filePath);


            if (encrypted)
            {
                using Stream inputStream = await ProfileParser.Write(profile);
                inputStream.Seek(0, SeekOrigin.Begin);
                using (Stream processed = await FileLoader.Encrypt(inputStream))
                {
                    if (processed == null)
                    {
                        Debug.WriteLine($"Failed to encrypt the json data.");
                        Debug.WriteLine("R.FAILED_TO_ENCRYPT_ERROR_MESSAGE");
                        return;
                    }
                    await WriteStreamToFileAsync(processed, filePath);
                }
            }
            else
            {
                using (Stream stream = await ProfileParser.Write(profile))
                {
                    await WriteStreamToFileAsync(stream, filePath);
                }
            }
        }

        private static async Task WriteStreamToFileAsync(Stream stream, string filePath)
        {
            if (!File.Exists(filePath)) File.WriteAllText(filePath, string.Empty);
            using (var filestream = new FileStream(filePath, FileMode.Truncate, FileAccess.Write))
            {
                stream.Seek(0, SeekOrigin.Begin);
                await stream.CopyToAsync(filestream);
            }
        }

        #endregion

        #region Save Old

        [Obsolete]
        public static async Task FileSaveAsync(string filePath, ProfileSaveFile profile)
        {
            if (filePath == null) { return; }
            profile.TotalGearPower = profile.computeCharacterPower();
            Console.WriteLine("Writing file: {0}", filePath);

            if (Path.GetExtension(filePath) == Constants.DECRYPTED_FILE_EXTENSION)
                await JsonFileSave(filePath, profile);
            else
                await DatFileSave(filePath, profile);


        }

        [Obsolete]
        private static async Task JsonFileSave(string filePath, ProfileSaveFile profile)
        {
            using var stream = await ProfileParser.Write(profile);
            await WriteStreamToFileAsync(stream, filePath);
        }

        [Obsolete]
        private static async Task DatFileSave(string filePath, ProfileSaveFile profile)
        {
            using var inputStream = await ProfileParser.Write(profile);
            inputStream.Seek(0, SeekOrigin.Begin);
            using Stream processed = await FileLoader.Encrypt(inputStream);
            if (processed == null)
            {
                Debug.WriteLine($"Failed to encrypt the json data.");
                Debug.WriteLine("R.FAILED_TO_ENCRYPT_ERROR_MESSAGE");
                return;
            }
            await WriteStreamToFileAsync(processed, filePath);
        }


        #endregion

        #region Load Old

        [Obsolete]
        public static async Task<ProfileSaveFile?> FileOpenAsync(string filePath)
        {
            Console.WriteLine("Reading file: {0}", filePath);
            if (Path.GetExtension(filePath) == Constants.DECRYPTED_FILE_EXTENSION)
                return await JsonFileOpen(filePath);
            else
                return await DatFileOpen(filePath);
        }


        [Obsolete]
        private static Task<ProfileSaveFile?> JsonFileOpen(string filePath)
        {
            using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return TryParseFileStreamAsync(stream);
        }

        [Obsolete]
        private static async Task<ProfileSaveFile?> DatFileOpen(string filePath)
        {
            var file = new FileInfo(filePath);
            using FileStream inputStream = file.OpenRead();
            bool encrypted = SaveFileHandler.IsFileEncrypted(inputStream);
            if (!encrypted)
            {
                Debug.WriteLine($"The file \"{file.Name}\" was in an unexpected format.");
                Debug.WriteLine("R.formatFILE_IN_UNEXPECTED_FORMAT_ERROR_MESSAGE(file.Name)");
                return null;
            }
            using Stream processed = await FileLoader.Decrypt(inputStream);
            if (processed == null)
            {
                Debug.WriteLine($"Content of file \"{file.Name}\" could not be converted to a supported format.");
                Debug.WriteLine("R.formatFILE_DECRYPT_ERROR_MESSAGE(file.Name)");
                return null;
            }
            return await TryParseFileStreamAsync(processed);
        }

        #endregion

        #region Old CMD Line Methods

        public static async ValueTask FileConvert(FileInfo file, bool overwrite)
        {
            if (!file.Exists)
            {
                EventLogger.logError($"File \"{file.FullName}\" could not be found and has been skipped.");
                return;
            }

            using FileStream inputStream = file.OpenRead();
            bool encrypted = SaveFileHandler.IsFileEncrypted(inputStream);

            Stream processed = encrypted ? await Decrypt(inputStream) : await Encrypt(inputStream);
            if (processed == null)
            {
                EventLogger.logError($"Content of file \"{file.Name}\" could not be converted to a supported format.");
                return;
            }

            processed.Seek(0, SeekOrigin.Begin);
            string outputFile = GetOutputFilePath(file, encrypted, overwrite);
            using FileStream outputStream = File.Open(outputFile, FileMode.Create, FileAccess.Write);
            await processed.CopyToAsync(outputStream);
        }
        private static string GetOutputFilePath(FileInfo fileInfo, bool isEncrypted, bool overwrite)
        {
            string targetExtension = fileInfo.Extension.ToUpperInvariant() switch
            {
                "" => isEncrypted ? ".json" : "", // Special case for Switch which has no file extension
                _ => isEncrypted ? ".json" : ".dat",
            };

            string idealFileName = $"{Path.GetFileNameWithoutExtension(fileInfo.Name)}{targetExtension}";
            if (string.Equals(fileInfo.Name, idealFileName, StringComparison.CurrentCultureIgnoreCase))
            {
                idealFileName = $"{Path.GetFileNameWithoutExtension(idealFileName)}_{(isEncrypted ? "Decrypted" : "Encrypted")}{targetExtension}";
            }

            string outFileName = Path.Combine(fileInfo.DirectoryName, idealFileName);
            if (overwrite || !File.Exists(outFileName))
            {
                return outFileName;
            }

            int fileNumber = 1;
            while (File.Exists(outFileName))
            {
                outFileName = $"{outFileName.Substring(outFileName.Length - targetExtension.Length)}_{fileNumber++}{targetExtension}";
            }

            return outFileName;
        }

        #endregion

    }
}
