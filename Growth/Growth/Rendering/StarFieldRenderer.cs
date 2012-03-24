using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Growth.GameObjects;
using Growth.Cameras;
using Microsoft.Xna.Framework;

namespace Growth.Rendering
{        
    public class StarFieldRenderer : IDisposable
    {
        private readonly CameraStack cameraStack;
        private readonly Random random;
        private readonly GraphicsDevice graphics;
        private readonly Vector2 planeDimension;
        private readonly IList<Texture2D> starTextures;

        private readonly Vector2 planeSpriteOrigin;

        private readonly SpriteBatch spriteBatch;

        private RenderTarget2D furthestField;
        private RenderTarget2D middleField;
        private RenderTarget2D nearField;
        
        public StarFieldRenderer(GraphicsDevice graphics, CameraStack cameraStack, params Texture2D[] starTextures)
        {
            this.cameraStack = cameraStack;
            this.starTextures = starTextures;
            this.graphics = graphics;
            this.random = new Random();
            furthestField = new RenderTarget2D(graphics, graphics.Viewport.Width, graphics.Viewport.Height);
            middleField = new RenderTarget2D(graphics, graphics.Viewport.Width, graphics.Viewport.Height);
            nearField = new RenderTarget2D(graphics, graphics.Viewport.Width, graphics.Viewport.Height);
            spriteBatch = new SpriteBatch(graphics);

            planeDimension = new Vector2(graphics.Viewport.Width, graphics.Viewport.Height);
            planeSpriteOrigin = planeDimension / 2f;

            GenerateStarFieldPlanes();
        }

        public void GenerateStarFieldPlanes()
        {
            GenerateStarField(furthestField, 200, 0.5f);
            GenerateStarField(middleField, 150, 0.75f);
            GenerateStarField(nearField, 120, 1f);
        }

        private void GenerateStarField(RenderTarget2D renderTarget, int numberOfStars, float scale)
        {
            graphics.SetRenderTarget(renderTarget);
            graphics.Clear(Color.Transparent);
            spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend);

            for (int i = 0; i < numberOfStars; i++)
            {
                int textureIndex = random.Next(starTextures.Count);
                Texture2D texture = starTextures[textureIndex];

                float x = (float)(random.NextDouble() * (planeDimension.X - texture.Width)) + (texture.Width / 2f);
                float y = (float)(random.NextDouble() * (planeDimension.Y - texture.Height)) + (texture.Height / 2f);


                spriteBatch.Draw(texture, new Vector2(x, y), Color.White);
            }

            spriteBatch.End();
            graphics.SetRenderTarget(null);
        }



        private void RenderFieldTextureAt(Texture2D fieldTexture, Vector2 position, Color color)
        {
            spriteBatch.Draw(fieldTexture, position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        private void RenderStarFieldPlane(Texture2D fieldTexture, Vector2 position, float movementScale)
        {
            position = position * movementScale;

            position.X %= planeDimension.X;
            position.Y %= planeDimension.Y;

            RenderFieldTextureAt(fieldTexture, position, Color.White);

            
            if (position.X > 0)
                RenderFieldTextureAt(fieldTexture, position + (-Vector2.UnitX * planeDimension), Color.Red);

            if (position.X < 0)
                RenderFieldTextureAt(fieldTexture, position + (Vector2.UnitX * planeDimension), Color.Magenta);

            if (position.Y > 0)
                RenderFieldTextureAt(fieldTexture, position + (-Vector2.UnitY * planeDimension), Color.Green);

            if (position.Y < 0)
                RenderFieldTextureAt(fieldTexture, position + (Vector2.UnitY * planeDimension), Color.Cyan);

            if (position.X > 0 && position.Y > 0)
                RenderFieldTextureAt(fieldTexture, position + (-Vector2.One * planeDimension), Color.Blue);

            if (position.X < 0 && position.Y > 0)
                RenderFieldTextureAt(fieldTexture, position + (new Vector2(1, -1) * planeDimension), Color.Yellow);

            if (position.X > 0 && position.Y < 0)
                RenderFieldTextureAt(fieldTexture, position + (new Vector2(-1, 1) * planeDimension), Color.Purple);
            
            if (position.X < 0 && position.Y < 0)
                RenderFieldTextureAt(fieldTexture, position + (Vector2.One * planeDimension), Color.SaddleBrown);

            

        }

        public void Render()
        {
            Vector2 position = Vector2.Zero;
            position = Vector2.Transform(position, cameraStack.Current.GetViewMatrix());
            position -= planeSpriteOrigin;

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);            

            RenderStarFieldPlane(furthestField, position, 0.5f);
            RenderStarFieldPlane(middleField, position, 0.75f);
            RenderStarFieldPlane(nearField, position, 1f);

            spriteBatch.End();
        }

        public void Dispose()
        {
            furthestField.Dispose();
        }
    }
}