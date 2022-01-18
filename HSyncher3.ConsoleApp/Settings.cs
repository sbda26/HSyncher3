using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSyncher3.ConsoleApp
{
    internal class Settings
    {
        public string SourceDirectoryRoot { get; set; }
        public string DestinationDirectoryRoot { get; set; }
        public bool? DeleteFilesInDestinationNotInSource { get; set; }
        public bool? RunAsynchronously { get; set; }
        public bool? BreakOnError { get; set; }
    }
}
