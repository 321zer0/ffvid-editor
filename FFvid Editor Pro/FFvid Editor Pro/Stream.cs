using System;
using System.Collections.Generic;

namespace FFvid_Editor_Pro
{
    abstract class Stream
    {
        protected string Filename { get; set; }
        protected int Index { get; set; }
        protected string Codec { get; set; }
        
        protected int Bitrate { get; set; }

        protected string Type { get; set; }
    }
}
