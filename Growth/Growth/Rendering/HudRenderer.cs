using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Growth.GameObjects.Entities;
using Microsoft.Xna.Framework;

namespace Growth.Rendering
{    
    public class HudRenderer
    {
        private readonly SpriteFont font;
        private readonly GraphicsDevice graphics;        
        private readonly SpriteBatch spriteBatch;        

        public HudRenderer(GraphicsDevice graphics, SpriteFont font)
        {
            if (graphics == null)
                throw new ArgumentNullException("graphics", "graphics is null.");
            if (font == null)
                throw new ArgumentNullException("font", "font is null.");            

            this.graphics = graphics;
            this.font = font;            
            this.spriteBatch = new SpriteBatch(graphics);
        }

        public Ship Ship { get; set; }
        public PlayerStats Stats { get; set; }

        public bool ShowGameOver { get; set; }

        public void Render()
        {
            if (Ship == null && Stats == null)
                return;

            spriteBatch.Begin();

            if (ShowGameOver)
            {
                const string str = "Game Over";

                Vector2 measure = font.MeasureString(str);
                measure /= 2f;
                Vector2 screenCenter =
                    new Vector2(graphics.Viewport.Width / 2f,
                                graphics.Viewport.Height / 2f);
                
                spriteBatch.DrawString(font, str, screenCenter - measure, Color.White);
            }
            else
            {
                if (Stats != null)
                    spriteBatch.DrawString(font, string.Format("Score:  {0}", Stats.Score), new Vector2(10, 10), Color.White);
                
                if (Ship != null)
                    spriteBatch.DrawString(font, string.Format("Health: {0}", Ship.Health), new Vector2(10, font.LineSpacing), Color.White);
            }

            spriteBatch.End();
        }
    }
}
