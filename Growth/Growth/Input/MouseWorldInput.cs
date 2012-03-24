using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Growth.Input
{        
    public class MouseWorldInput
    {
        private readonly GraphicsDevice graphics;

        public MouseWorldInput(GraphicsDevice graphics)
        {
            this.graphics = graphics;
        }

        public Vector2 GetMouseWorldPosition()
        {
            MouseState mouseState = Mouse.GetState();
            Vector3 screenMousePosition = new Vector3(mouseState.X, mouseState.Y, 0);

            Matrix projection = Matrix.CreateOrthographicOffCenter(0, graphics.Viewport.Width, graphics.Viewport.Height, 0, 0, 1);

            Matrix view;
            Matricies.GetWorldToViewMatrix(graphics.Viewport, out view);

            Vector3 unprojected = graphics.Viewport.Unproject(screenMousePosition, projection, view, Matrix.Identity);

            return new Vector2(unprojected.X, unprojected.Y);
        }
    }
}
