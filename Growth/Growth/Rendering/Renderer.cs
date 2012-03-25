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
        public const float TexturePixelsPerUnit = 32;
        public const float TextureWorldScale = 1 / TexturePixelsPerUnit;

        private readonly GraphicsDevice graphics;
        private readonly SpriteBatch spriteBatch;
        private readonly List<Sprite> sprites;

        public Renderer(GraphicsDevice graphics, CameraStack cameraStack)
        {
            if (graphics == null)
                throw new ArgumentNullException("graphics", "graphicsDevice is null.");
            if (cameraStack == null)
                throw new ArgumentNullException("cameraStack", "cameraStack is null.");

            this.cameraStack = cameraStack;
            this.graphics = graphics;
            this.spriteBatch = new SpriteBatch(graphics);
            this.sprites = new List<Sprite>();
        }

        public void AddSprite(Sprite newSprite)
        {
            this.sprites.Add(newSprite);
        }

        public void ClearSprites()
        {
            this.sprites.Clear();
        }

        public void Render()
        {
            if (this.sprites.Count == 0)
                return;

            Matrix matrix = cameraStack.Current.GetViewMatrix();            

            spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend, null, null, null, null, matrix);
            for (int i = 0; i < sprites.Count; i++)
            {
                spriteBatch.Draw(sprites[i].Texture, sprites[i].Position, sprites[i].SourceRectangle, sprites[i].Tint, sprites[i].Rotation, sprites[i].Origin, TextureWorldScale, SpriteEffects.None, 0f);
            }

            spriteBatch.End();
        }

        public void RemoveSprite(Sprite sprite)
        {
            sprites.Remove(sprite);
        }
    }
}
