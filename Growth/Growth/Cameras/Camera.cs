using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Growth.Cameras
{
    public interface Camera
    {
        Matrix GetViewMatrix();
        void Update(double dt);
    }
}
