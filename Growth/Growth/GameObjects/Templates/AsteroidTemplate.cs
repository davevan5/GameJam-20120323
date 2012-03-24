using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Growth.Input;

namespace Growth.GameObjects.Templates
{
    public class AsteroidTemplate : ITemplate
    {
        private ContentManager content;
        private MouseWorldInput mouseInput;
        private EntityConstructor entityConstructor;

        public AsteroidTemplate(EntityConstructor entityConstructor, ContentManager content, MouseWorldInput mouseInput)
        {
            this.content = content;
            this.mouseInput = mouseInput;
            this.entityConstructor = entityConstructor;
        }

        public Entity Make()
        {
            Sprite asteroidSprite = new Sprite(content.Load<Texture2D>("Sprites\\Asteroid1"), new Vector2(16f, 16f));
            return new Asteroid(asteroidSprite);
        }
    }
}
