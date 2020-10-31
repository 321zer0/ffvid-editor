using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace FFvid_Editor_Pro
{
    class MediaFile
    {
        List<Stream> _streams = new List<Stream>();

        public MediaFile(string filename)
        {
			if (!File.Exists(filename))
            {
                MessageBox.Show("Error: The file '" + filename + "' could not be found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // TODO: throw FileNotFoundException
                return;
            }

            // TODO: check if FFmpeg exists, if not, we need to abort

            this.Filename = filename;

            GetMediaInfo();
        }

        private void GetMediaInfo()
        {
            Process process = new Process();
            process.StartInfo.FileName = "ffmpeg.exe";
            process.StartInfo.Arguments = @"-i """ + this.Filename + @"""";

            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true; 
            process.OutputDataReceived += new DataReceivedEventHandler(DataReceivedHandler);
            process.ErrorDataReceived += new DataReceivedEventHandler(DataReceivedHandler);
 
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();

            process.Dispose();
        }

        private void DataReceivedHandler(object process, DataReceivedEventArgs lineReceived)
        {   
            if (lineReceived.Data == null || lineReceived.Data.Length == 0)
            {
                return;
            }

            string line = lineReceived.Data.Trim();
			
			if (line.Contains("Invalid data found when processing"))
            {
                MessageBox.Show("Error: The file '" + this.Filename + "' could not be processed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // FIXME: There should be a way to notify if media probe was unsucessful
            }

            if (line.StartsWith("Duration: "))
            {
                string[] fields = line.Split(' ');

                // Retrieve Duration
                string[] duration = fields[1].Split(':');
                int hours = int.Parse(duration[0]);
                int minutes = int.Parse(duration[1]);
                double seconds = double.Parse(duration[2].Split('.')[0]); // FIXME: retrieve decimal portion of second
                seconds = (hours * 3600) + (minutes * 60) + seconds;
                this.Duration = TimeSpan.FromSeconds(seconds);

                // Retrieve Bitrate
                if (fields[6].ToLower() == "kb/s")
                {
                    this.Bitrate = int.Parse(fields[5]);
                }
                else if (fields[6].ToLower() == "mb/s")
                {
                    this.Bitrate = int.Parse(fields[5]) * 1000;
                }
            }

            // TODO: Retrieve Subtitle stream as well
            if (line.StartsWith("Stream #0:"))
            {
                string[] fields = line.Split(' ');

                // Retrieve Stream Index
                string s_index = fields[1].Split(':')[1];
                int index = s_index.IndexOf('(');
                index = int.Parse(s_index.Substring(0, index));

                // Video Stream
                if (fields[2] == "Video:")
                {
                    string v_codec = fields[3];
                    // TODO: retrieve video profile [4]
                    string pixfmt = fields[8].Replace(",", "");
                    int width = int.Parse(fields[9].Split('x')[0]);
                    int height = int.Parse(fields[9].Split('x')[1].Replace(",", ""));
                    int v_bitrate = int.Parse(fields[10]);
                    string v_bitrate_unit = fields[11];
                    int framerate = int.Parse(fields[12]);

                    if (v_bitrate_unit.ToLower() == "mb/s")
                    {
                        v_bitrate = v_bitrate * 1000;
                    }

                    VideoStream sv = new VideoStream(this.Filename, index, v_codec, pixfmt, width, height, v_bitrate, framerate);
                    _streams.Add(sv);
                }
                // Audio Stream
                else if (fields[2] == "Audio:")
                {
                    string a_codec = fields[3];
                    int samplingRate = int.Parse(fields[8]);
                    string channel = fields[10].Replace(",", "");
                    int a_bitrate = int.Parse(fields[12]);
                    string a_bitrate_unit = fields[13];                    

                    if (a_bitrate_unit.ToLower() == "mb/s")
                    {
                        a_bitrate = a_bitrate * 1000;
                    }

                    AudioStream sa = new AudioStream(this.Filename, index, a_codec, samplingRate, channel, a_bitrate);
                    _streams.Add(sa);
                }
            }
        }

        public string Filename { get; private set; }

        public TimeSpan Duration { get; private set; }
        
        public int Bitrate { get; private set; }

        public List<Stream> Streams { get { return _streams;  } }
    }
}