using System;
using System.Collections.Generic;
using System.Linq;

namespace Growth.Animations
{
    public class Animation
    {
        public Animation(string name, int fps, int startFrame, int endFrame, bool loop)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentException("name is null or empty.", "name");

            Name = name;
            this.Loop = loop;
            this.EndFrame = endFrame;
            this.StartFrame = startFrame;
            this.Fps = fps;
        }

        public int Fps { get; private set; }
        public int EndFrame { get; private set; }
        public int StartFrame { get; private set; }
        public bool Loop { get; private set; }
        public string Name { get; private set; }
    }
}
