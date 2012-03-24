using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Growth.Cameras
{
    public class NullCamera : Camera
    {
        private readonly GraphicsDevice graphics;

        private Matrix matrix;

        public NullCamera(GraphicsDevice graphics)
        {
            this.graphics = graphics;
        }

        public Matrix GetViewMatrix()
        {
            return matrix;
        }

        public void Update(double dt)
        {            
            Matricies.GetWorldToViewMatrix(graphics.Viewport, out matrix);
        }
    }
}
