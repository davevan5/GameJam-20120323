using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Growth.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Growth.GameObjects.Entities;

namespace Growth.Cameras
{
    public class FollowCamera : Camera
    {
        private readonly GraphicsDevice graphics;

        private Matrix matrix;

        public FollowCamera(GraphicsDevice graphics)
        {
            this.graphics = graphics;
        }

        public Ship Ship { get; set; }

        public Matrix GetViewMatrix()
        {
            return matrix;
        }

        public void Update(double dt)
        {
            Vector2 position = Ship == null ? Vector2.Zero : Ship.Position;

            Matrix worldToView;
            Matricies.GetWorldToViewMatrix(graphics.Viewport, out worldToView);

            matrix = Matrix.CreateTranslation(new Vector3(-position, 0)) * worldToView;
        }
    }
}
