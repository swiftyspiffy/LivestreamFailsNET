using System;
using System.Collections.Generic;
using System.Text;

namespace LivestreamFailsNET.Models
{
    public class Fail
    {
        public string Title { get; internal set; } = "";
        public string Url { get; internal set; } = "";
        public string Streamer { get; internal set; } = "";
        public string Game { get; internal set; } = "";
        public int Points { get; internal set; }
        public bool NSFW { get; internal set; }
        public string ThumbnailUrl { get; internal set; } = "";
        public string Id { get; internal set; } = "";
        public TimeSpan TimeSinceCreated { get; internal set; } = TimeSpan.FromSeconds(1);
    }
}
