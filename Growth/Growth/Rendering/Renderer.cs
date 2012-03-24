using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Growth.GameObjects;
    
namespace Growth.Rendering
{
    public class Renderer
    {
        const float texturePixelsPerUnit = 32;

        private readonly GraphicsDevice graphics;
        private readonly SpriteBatch spriteBatch;

        public Renderer(GraphicsDevice graphics)
        {
            this.graphics = graphics;
            this.spriteBatch = new SpriteBatch(graphics);
        }

        public Ship Ship { get; set; }

        public void Render()
        {
            if (Ship == null)
                return;

            Matrix matrix;
            Matricies.GetWorldToViewMatrix(graphics.Viewport, out matrix);

            spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend, null, null, null, null, matrix);
            spriteBatch.Draw(Ship.Texture, Ship.Position, null, Color.White, Ship.Rotation, Ship.Origin, 1 / texturePixelsPerUnit, SpriteEffects.None, 0f);
            spriteBatch.End();
        }
    }
}
