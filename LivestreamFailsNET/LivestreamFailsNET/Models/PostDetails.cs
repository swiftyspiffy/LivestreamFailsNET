using System;
using System.Collections.Generic;
using System.Text;

namespace LivestreamFailsNET.Models
{
    public class PostDetails
    {
        public string Title { get; internal set; }
        public string Streamer { get; internal set; }
        public string Game { get; internal set; }
        public int Points { get; internal set; }
        public bool NSFW { get; internal set; }
        public string VideoUrl { get; internal set; }
        public string SourceUrl { get; internal set; }
    }
}
