using System;
using System.IO;
using System.Linq;

namespace HSyncher3.Library
{
    public class HSyncher3Library
    {
        // CUSTOM EVENTS: https://stackoverflow.com/questions/6644247/simple-custom-event, https://docs.microsoft.com/en-us/dotnet/standard/events/how-to-raise-and-consume-events
        // VIRTUAL METHODS: https://stackoverflow.com/questions/622132/what-are-virtual-methods
        // DELEGATES: https://www.tutorialspoint.com/csharp/csharp_delegates.htm


        public TotalFileCopyStatistics TotalFileCopyStatistics { get; private set; }  // allow total stats to be passed to both property and event

        public void Go(string sourceRootDirectory, string destinationRootDirectory, bool deleteDestNotInSource, bool breakOnError)
        {
            string[] sourceFiles = AllFiles(sourceRootDirectory);

            var args = new AllFilesEventArgs { FilesToCopy = sourceFiles };
            RaiseFilesToCopyEvent(args);

            if (Directory.Exists(destinationRootDirectory) == false)
                Directory.CreateDirectory(destinationRootDirectory);

            string[] destinationFiles = AllFiles(destinationRootDirectory);
            string[] sourceFilesNotInDestination;

            if (deleteDestNotInSource == true)
                DeleteDestNotInSource(sourceFiles, destinationRootDirectory, ref destinationFiles);
            
            sourceFilesNotInDestination = sourceFiles.Except(destinationFiles).ToArray();
            Copy(sourceRootDirectory, destinationRootDirectory, sourceFilesNotInDestination, breakOnError);
        }

        private string[] AllFiles(string path)
        {
            string[] allFiles = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

            for (int index = 0; index <= allFiles.GetUpperBound(0); index++)
            {
                allFiles[index] = allFiles[index].Replace(path, string.Empty).ToLower();
                if (allFiles[index].StartsWith(@"\") == true)
                    allFiles[index] = allFiles[index][1..];
            }

            return allFiles;
        }

        private void DeleteDestNotInSource(string[] source_Files, string destinationRootDirectory, ref string[] destination_Files)
        {
            string[] destNotInSource = destination_Files.Except(source_Files).ToArray();

            if (destNotInSource != null)
            {
                string[] refinedDestinationFiles = destination_Files.Except(destNotInSource).ToArray();
                foreach (string file in destNotInSource)
                {
                    string backSlash = Backslash(destinationRootDirectory);
                    string fileToDelete = string.Format("{0}{1}{2}", destinationRootDirectory, backSlash, file);
                    if (new FileInfo(fileToDelete).Length < uint.MaxValue)
                    {
                        RemoveReadOnlyAttribute(fileToDelete);
                        File.Delete(fileToDelete);
                    }
                }
                destination_Files = refinedDestinationFiles;
            }
        }

        private void RemoveReadOnlyAttribute(string fileToDelete)
        {
            FileAttributes fa = File.GetAttributes(fileToDelete);

            if ((fa | FileAttributes.ReadOnly) > 0)
                File.SetAttributes(fileToDelete, FileAttributes.Normal);
        }

        private void Copy(string sourceRootDirectory, string destinationRootDirectory, string[] sourceFilesNotInDestination, bool breakOnError)
        {
            var totalStats = new TotalFileCopyStatistics { StartTime = DateTime.Now };
            string backslashSource = Backslash(sourceRootDirectory);
            string backslashDestination = Backslash(destinationRootDirectory);

            foreach (string file in sourceFilesNotInDestination)
            {
                var source = new FileInfo($"{sourceRootDirectory}{backslashSource}{file}");
                string destination = $"{destinationRootDirectory}{backslashDestination}{file}";
                DateTime fileCopyStartTime = DateTime.Now;
                DateTime currentTimeAfterFileCopy;

                try
                {
                    CreateDirectory(destination);
                    source.CopyTo(destination, false);
                    currentTimeAfterFileCopy = DateTime.Now;
                    totalStats.FilesSuccessfullyCopied++;
                    totalStats.BytesSuccessfullyCopied += source.Length;
                    totalStats.Milliseconds += (currentTimeAfterFileCopy - fileCopyStartTime).Milliseconds;
                    RaiseFileCopiedEvent(new FileCopiedEventArgs
                    {
                        FileCopyFinishTime = currentTimeAfterFileCopy,
                        FileCopyStartTime = fileCopyStartTime,
                        Source = source.FullName,
                        Destination = destination,
                        Length = source.Length
                    });
                }
                catch (Exception ex)
                {
                    if (breakOnError)
                        throw new Exception(ex.Message);
                    else
                    {
                        totalStats.Milliseconds += (DateTime.Now - fileCopyStartTime).Milliseconds;
                        totalStats.FilesUnsuccessfullyCopied++;
                        totalStats.BytesUnsuccessfullyCopied += source.Length;
                        RaiseFileCopyErrorEvent(new FileCopyErrorEventArgs { ErrorException = ex });
                    }
                }
            }
            totalStats.EndTime = DateTime.Now;
            TotalFileCopyStatistics = totalStats;
            RaiseFilesCopiedDoneEvent(new FilesCopiedDoneEventArgs { TotalFileCopyStatistics = totalStats });
        }

        protected virtual void RaiseFilesToCopyEvent(AllFilesEventArgs e) => FilesToCopyEvent?.Invoke(this, e);

        protected virtual void RaiseFileCopiedEvent(FileCopiedEventArgs e) => FileCopiedEvent?.Invoke(this, e);

        protected virtual void RaiseFileCopyErrorEvent(FileCopyErrorEventArgs e) => FileCopyErrorEvent?.Invoke(this, e);

        protected virtual void RaiseFilesCopiedDoneEvent(FilesCopiedDoneEventArgs e) => FilesCopiedDoneEvent?.Invoke(this, e);

        /*
        protected virtual void RaiseFilesToCopyEvent(AllFilesEventArgs e)
        {
            EventHandler<AllFilesEventArgs> handler = FilesToCopyEvent;

            if (handler != null)
                handler(this, e);
        }
        */

        private string Backslash(string path) => path.EndsWith(@"\") == false ? @"\" : string.Empty;

        private void CreateDirectory(string destination)
        {
            var fiDest = new FileInfo(destination);

            if (fiDest.Directory.Exists == false)
                fiDest.Directory.Create();
        }

        public event EventHandler<AllFilesEventArgs> FilesToCopyEvent;
        public event EventHandler<FileCopiedEventArgs> FileCopiedEvent;
        public event EventHandler<FileCopyErrorEventArgs> FileCopyErrorEvent;
        public event EventHandler<FilesCopiedDoneEventArgs> FilesCopiedDoneEvent;
    }

    public class AllFilesEventArgs : EventArgs
    {
        public string[] FilesToCopy { get; set; }
    }

    public class FileCopiedEventArgs : EventArgs
    {
        public string Source { get; set; }
        public string Destination { get; set; }
        public long Length { get; set; }
        public DateTime FileCopyStartTime { get; set; }
        public DateTime FileCopyFinishTime { get; set; }
    }

    public class FileCopyErrorEventArgs : EventArgs
    {
        public Exception ErrorException { get; set; }
    }

    public class FilesCopiedDoneEventArgs : EventArgs
    {
        public TotalFileCopyStatistics TotalFileCopyStatistics { get; set; }
    }
}
