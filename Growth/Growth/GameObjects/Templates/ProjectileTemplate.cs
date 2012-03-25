﻿using System;
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
    public class ProjectileTemplate : ITemplate
    {
        private ContentManager content;
        private EntityConstructor entityConstructor;

        public ProjectileTemplate(EntityConstructor entityConstructor, ContentManager content)
        {
            this.content = content;
            this.entityConstructor = entityConstructor;
        }

        public Entity Make()
        {
            Sprite projectileSprite = new Sprite(content.Load<Texture2D>("Sprites\\Projectile"), new Vector2(16f, 16f));
            return new Projectile(projectileSprite)
            {
                CanCollide = true,
                IsPhysical = false,
                IsStatic = false,                
                CollisionRadius = 0.8f
            };
        }
    }
}
