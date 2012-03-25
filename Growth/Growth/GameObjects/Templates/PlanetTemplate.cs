using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Growth.Input;
using Growth.GameObjects.Entities;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Growth.GameObjects.Templates
{
    public class PlanetTemplate : ITemplate
    {
        private ContentManager content;
        private EntityConstructor entityConstructor;

        public PlanetTemplate(EntityConstructor entityConstructor, ContentManager content)
        {
            this.content = content;
            this.entityConstructor = entityConstructor;
        }

        public Entity Make()
        {
            Sprite planetSprite = new Sprite(content.Load<Texture2D>("Sprites\\Planet"), new Vector2(160f, 160f));
            return new Planet(planetSprite)
            {
                CanCollide = true,
                IsPhysical = true,
                IsStatic = true,
                CollisionRadius = 5f
            };
        }
    }
}
