using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
    
namespace Growth.Rendering
{
    public class Renderer
    {
        private readonly GraphicsDevice graphics;
        private readonly SpriteBatch spriteBatch;

        public Renderer(GraphicsDevice graphics)
        {
            this.graphics = graphics;
            this.spriteBatch = new SpriteBatch(graphics);
        }

        public Ship Ship { get; set; }

        public void WorldToViewMatrix(out Matrix matrix)
        {
            const int smallestSqaureUnitSize = 40;
            const int texturePixelsPerUnit = 32;
            // with sprite batch, the view is not in world coordinates, it's in pixel coordinates
            // so we need to figure out how to get there
            // so we need to figure out how much units the screen represents
            // so lets take the smallest square the screen can make and assign it world unit size
            float smallestDimension = Math.Min(graphics.Viewport.Width, graphics.Viewport.Height);
            float screenPixelsPerUnit = smallestDimension / smallestSqaureUnitSize;

            Matrix result = Matrix.Identity;
            result = Matrix.CreateScale(1f / texturePixelsPerUnit);
            result *= Matrix.CreateScale(screenPixelsPerUnit);
            result *= Matrix.CreateTranslation(new Vector3(graphics.Viewport.Width / 2f, graphics.Viewport.Height / 2f, 0));
            matrix = result;
        }

        public void Render()
        {
            if (Ship == null)
                return;

            Matrix matrix;
            WorldToViewMatrix(out matrix);

            spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend, null, null, null, null, matrix);
            spriteBatch.Draw(Ship.Texture, Ship.Position, Color.White);
            spriteBatch.End();
        }
    }
}
