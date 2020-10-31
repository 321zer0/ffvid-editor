using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFvid_Editor_Pro
{
    class AudioStream : Stream
    {
        public AudioStream(string filename, int index, string codec, int sampleRate, string channel, int bitrate)
        {
            this.Type = "Audio";
            this.Filename = filename;
            this.Index = index;
            this.Codec = codec;
            this.SampleRate = sampleRate;
            this.Channel = channel;
            this.Bitrate = bitrate;
        }

        public int SampleRate { get; set; }
        
        public string Channel { get; set; }
    }
}