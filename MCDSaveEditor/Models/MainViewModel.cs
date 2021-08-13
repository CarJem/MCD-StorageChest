using DungeonTools.Save.File;
using MCDSaveEditor.Save.Profiles;
using MCDSaveEditor.Save.Json;
using MCDSaveEditor.Extensions;
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
using MCDSaveEditor;
using MCDSaveEditor.Save;
using System.ComponentModel;
using MCDSaveEditor.Logic;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows;
#nullable disable

namespace MCDSaveEditor.Models
{
    public class MainViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private ProfileSaveFile _CurrentSaveFile;
        private Item _CurrentItem;
        private Save.Enums.ItemFilterEnum _CurrentFiler = Save.Enums.ItemFilterEnum.All;
        void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        public MainViewModel(MainWindow host)
        {
            Host = host;
        }

        private MainWindow Host { get; set; }

        public ProfileSaveFile CurrentSaveFile
        {
            get { return _CurrentSaveFile; }
            set 
            { 
                _CurrentSaveFile = value;
                OnPropertyChanged(nameof(CurrentSaveFile)); 
            }
        }
        public Item CurrentItem
        {
            get { return _CurrentItem; }
            set { _CurrentItem = value; OnPropertyChanged(nameof(CurrentItem)); }
        }

        public Save.Enums.ItemFilterEnum CurrentFilter
        {
            get { return _CurrentFiler; }
            set { _CurrentFiler = value; OnPropertyChanged(nameof(CurrentFilter)); }
        }

        public async Task FileOpenAsync(string filePath)
        {
            Console.WriteLine("Reading file: {0}", filePath);
            if (Path.GetExtension(filePath) == Constants.DECRYPTED_FILE_EXTENSION)
                CurrentSaveFile = await JsonFileOpen(filePath);
            else
                CurrentSaveFile = await DatFileOpen(filePath);

            Task<ProfileSaveFile> JsonFileOpen(string filePath)
            {
                using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                return TryParseFileStreamAsync(stream);
            }

            async Task<ProfileSaveFile> DatFileOpen(string filePath)
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
                using Stream processed = await FileProcessHelper.Decrypt(inputStream);
                if (processed == null)
                {
                    Debug.WriteLine($"Content of file \"{file.Name}\" could not be converted to a supported format.");
                    Debug.WriteLine("R.formatFILE_DECRYPT_ERROR_MESSAGE(file.Name)");
                    return null;
                }
                return await TryParseFileStreamAsync(processed!);
            }

            async Task<ProfileSaveFile> TryParseFileStreamAsync(Stream stream)
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
        }
        public async Task FileSaveAsync(string filePath, ProfileSaveFile profile)
        {
            if (filePath == null) { return; }
            profile.TotalGearPower = profile.computeCharacterPower();
            Console.WriteLine("Writing file: {0}", filePath!);

            if (Path.GetExtension(filePath!) == Constants.DECRYPTED_FILE_EXTENSION)
                await JsonFileSave(filePath!, profile);
            else
                await DatFileSave(filePath!, profile);

            async Task JsonFileSave(string filePath, ProfileSaveFile profile)
            {
                using var stream = await ProfileParser.Write(profile);
                await WriteStreamToFileAsync(stream, filePath);
            }

            async Task DatFileSave(string filePath, ProfileSaveFile profile)
            {
                using var inputStream = await ProfileParser.Write(profile);
                inputStream.Seek(0, SeekOrigin.Begin);
                using Stream processed = await FileProcessHelper.Encrypt(inputStream);
                if (processed == null)
                {
                    Debug.WriteLine($"Failed to encrypt the json data.");
                    Debug.WriteLine("R.FAILED_TO_ENCRYPT_ERROR_MESSAGE");
                    return;
                }
                await WriteStreamToFileAsync(processed, filePath);
            }

            async Task WriteStreamToFileAsync(Stream stream, string filePath)
            {
                if (!File.Exists(filePath))
                {
                    File.WriteAllText(filePath, string.Empty);
                }
                using var filestream = new FileStream(filePath, FileMode.Truncate, FileAccess.Write);
                stream.Seek(0, SeekOrigin.Begin);
                await stream.CopyToAsync(filestream);
            }
        }
    }
}
