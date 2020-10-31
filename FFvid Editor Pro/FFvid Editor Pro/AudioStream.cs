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

        public int ChannelCount
        { 
            get
            {
                if (this.Channel == "mono")
                {
                    return 1;
                }
                else if (this.Channel == "stereo")
                {
                    return 2;
                }
                else
                {
                    return 0;
                }
            }
        }

        public override string ToString()
        {
            return "Stream Type: " + this.Type + "\n" +
                   "Index: " + this.Index.ToString() + "\n" +
                   "Codec: " + this.Codec + "\n" +
                   "Sample rate: " + this.SampleRate + " Hz" + "\n" +
                   "Channel: " + this.Channel + "\n" +
                   "Channel count: " + this.ChannelCount.ToString() + "\n" +
                   "Bitrate: " + this.Bitrate + " kbps";
        }
    }
}