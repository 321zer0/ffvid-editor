using System;
using System.Collections.Generic;

namespace FFvid_Editor_Pro
{
    class VideoStream : Stream
    {
        public VideoStream(string filename, int index, string codec, string pixfmt, int width, int height, int bitrate, int framerate)
        {
            this.Type = "Video";
            this.Filename = filename;
            this.Index = index;
            this.Codec = codec;
            this.PixelFormat = pixfmt;
            this.Width = width;
            this.Height = height;
            this.Bitrate = bitrate;
            this.FrameRate = framerate;
        }

        public int Width { get; private set; }

        public int Height { get; private set; }
        
        public string PixelFormat { get; private set; }
        
        public float FrameRate { get; private set; }
    }
}
