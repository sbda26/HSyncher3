using HSyncher3.Library;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace HSyncher3.ConsoleApp
{
    class Program
    {
        private const string _configFile = "HSyncher3.ConsoleApp.Json";
        private static Settings _settings;
        private static HSyncher3Library _synch = new();

        static void Main(string[] args)
        {
            SetupEvents();
            InitSettings();
            GetSettingsFromUser();

            if (_settings.RunAsynchronously == false)
                _synch.Go(_settings.SourceDirectoryRoot, _settings.DestinationDirectoryRoot, _settings.DeleteFilesInDestinationNotInSource.Value, _settings.BreakOnError.Value);
            else
                Task.Run(() => _synch.Go(_settings.SourceDirectoryRoot, _settings.DestinationDirectoryRoot, _settings.DeleteFilesInDestinationNotInSource.Value, _settings.BreakOnError.Value)).Wait();
        }

        static string GetStringValue(string message, string defaultValue)
        {
            bool defaultValueExists = !string.IsNullOrEmpty(defaultValue);
            bool resultOK;
            string result;

            do
            {
                Console.Write($"{message}");
                if (defaultValueExists == true)
                    Console.Write($" [{defaultValue}]: ");
                else
                    Console.Write(": ");
                result = Console.ReadLine();
                resultOK = (string.IsNullOrEmpty(result) == false) || (string.IsNullOrEmpty(result) == true && defaultValueExists == true);
            } while (resultOK == false);

            if (string.IsNullOrEmpty(result) == true && defaultValueExists == true)
                result = defaultValue;

            return result;
        }

        static bool GetBoolValue(string message, bool? defaultValue)
        {
            bool resultOK;
            string result;

            do
            {
                Console.Write($"{message} (Y/N)");
                if (defaultValue.HasValue == true)
                {
                    if (defaultValue == true)
                        Console.Write(" [Y]?: ");
                    else
                        Console.Write(" [N]?: ");
                }
                else
                    Console.Write("?: ");
                result = Console.ReadLine().ToUpper().Trim();
                resultOK = (string.IsNullOrEmpty(result) == true && defaultValue.HasValue == true) ||
                           (string.IsNullOrEmpty(result) == false && (result == "Y" || result == "N"));
            } while (resultOK == false);

            if (string.IsNullOrEmpty(result) == true && defaultValue.HasValue == true)
                return defaultValue.Value;
            else
                return (result == "Y") ? true : false;
        }

        static void InitSettings()
        {
            if (File.Exists(_configFile) == true)
            {
                string json = File.ReadAllText(_configFile);
                _settings = JsonConvert.DeserializeObject<Settings>(json);
            }
            else
                _settings = new();
        }

        static void GetSettingsFromUser()
        {
            _settings.SourceDirectoryRoot = GetStringValue("Source directory root", _settings.SourceDirectoryRoot);
            _settings.DestinationDirectoryRoot = GetStringValue("Destination directory root", _settings.DestinationDirectoryRoot);
            _settings.BreakOnError = GetBoolValue("Break on error", _settings.BreakOnError);
            _settings.DeleteFilesInDestinationNotInSource = GetBoolValue("Delete files in destination not in source", _settings.DeleteFilesInDestinationNotInSource);
            _settings.RunAsynchronously = GetBoolValue("Run asynchronously", _settings.RunAsynchronously);
        }

        static void SaveSettings()
        {
            string json = JsonConvert.SerializeObject(_settings, Formatting.Indented);
            File.WriteAllText(_configFile, json);
        }

        static void SetupEvents()
        {
            _synch.FilesToCopyEvent += Synch_FilesToCopyEvent;
            _synch.FileCopiedEvent += Synch_FileCopiedEvent;
            _synch.FilesCopiedDoneEvent += Synch_FilesCopiedDoneEvent;
        }

        private static void Synch_FilesCopiedDoneEvent(object sender, FilesCopiedDoneEventArgs e)
        {
            PrintStats("EVENT", e.TotalFileCopyStatistics);
            SaveSettings();
            Environment.Exit(0);
        }

        private static void Synch_FileCopiedEvent(object sender, FileCopiedEventArgs e) =>
            Console.WriteLine($"File copied: {e.Source}, Size: {e.Length}.");

        private static void Synch_FilesToCopyEvent(object sender, AllFilesEventArgs e) =>
            Console.WriteLine($"# of files to copy: {e.FilesToCopy.Length}.");

        private static void PrintStats(string source, TotalFileCopyStatistics stats)
        {
            Console.WriteLine($"FROM {source}:");
            Console.WriteLine();
            Console.WriteLine($"BYTES SUCCESSFULLY COPIED: {stats.BytesSuccessfullyCopied}");
            Console.WriteLine($"BYTES UNSUCCESSFULLY COPIED: {stats.BytesUnsuccessfullyCopied}");
            Console.WriteLine($"END TIME: {stats.EndTime}");
            Console.WriteLine($"FILES SUCCESSFULLY COPIED: {stats.FilesSuccessfullyCopied}");
            Console.WriteLine($"FILES UNSUCCESSFULLY COPIED: {stats.FilesUnsuccessfullyCopied}");
            Console.WriteLine($"MILLISECONDS: {stats.Milliseconds}");
            Console.WriteLine($"START TIME: {stats.StartTime}");
            Console.WriteLine("==================================================================================");
        }

        /*
        private static string GetValueString(string message)
        {
        }

        private static bool GetValueYN(string message)
        {

        }
        */
    }
}
