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

        public override string ToString()
        {
            return "Stream Type: " + this.Type + "\n" +
                   "Index: " + this.Index.ToString() + "\n" +
                   "Codec: " + this.Codec + "\n" +
                   "Pixel format: " + this.PixelFormat + "\n" +
                   "Frame rate: " + this.FrameRate + " fps" + "\n" +
                   "Resolution: " + this.Width.ToString() + "x" + this.Height.ToString() + "\n" +
                   "Bitrate: " + this.Bitrate + " kbps";
        }
    }
}
