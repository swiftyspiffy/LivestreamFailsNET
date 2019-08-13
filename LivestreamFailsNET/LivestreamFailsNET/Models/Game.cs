using System;
using System.Collections.Generic;
using System.Text;

namespace LivestreamFailsNET.Models
{
    public class Game
    {
        public string Name { get; internal set; }
        public int Fails { get; internal set; }
        public string ImageUrl { get; internal set; }
    }
}
