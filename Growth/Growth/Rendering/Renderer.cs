using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Growth.GameObjects;
using Growth.Cameras;
    
namespace Growth.Rendering
{
    public class Renderer
    {
        private readonly CameraStack cameraStack;
        const float texturePixelsPerUnit = 32;

        private readonly GraphicsDevice graphics;
        private readonly SpriteBatch spriteBatch;

        public Renderer(GraphicsDevice graphics, CameraStack cameraStack)
        {
            if (graphics == null)
                throw new ArgumentNullException("graphics", "graphicsDevice is null.");
            if (cameraStack == null)
                throw new ArgumentNullException("cameraStack", "cameraStack is null.");

            this.cameraStack = cameraStack;
            this.graphics = graphics;
            this.spriteBatch = new SpriteBatch(graphics);
        }

        public Ship Ship { get; set; }

        public void Render()
        {
            if (Ship == null)
                return;

            Matrix matrix = cameraStack.Current.GetViewMatrix();            

            spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend, null, null, null, null, matrix);
            spriteBatch.Draw(Ship.Texture, Ship.Position, null, Color.White, Ship.Rotation, Ship.Origin, 1 / texturePixelsPerUnit, SpriteEffects.None, 0f);
            spriteBatch.End();
        }
    }
}
