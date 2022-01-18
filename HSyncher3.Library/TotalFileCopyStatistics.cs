using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSyncher3.Library
{
    public class TotalFileCopyStatistics
    {
        public int FilesSuccessfullyCopied { get; set; }
        public int FilesUnsuccessfullyCopied { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double Milliseconds { get; set; }
        public long BytesSuccessfullyCopied { get; set; }
        public long BytesUnsuccessfullyCopied { get; set; }
    }
}
