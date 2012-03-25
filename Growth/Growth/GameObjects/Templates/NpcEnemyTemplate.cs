﻿using Growth.GameObjects;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Growth.Input;
using Microsoft.Xna.Framework;
using Growth.GameObjects.Entities;

namespace Growth.GameObjects.Templates
{
    public class NpcEnemyTemplate : ITemplate
    {
        private ContentManager content;
        private EntityConstructor entityConstructor;
        private readonly PlayerStats stats;
        
        public NpcEnemyTemplate(EntityConstructor entityConstructor, ContentManager content, PlayerStats stats)
        {
            this.stats = stats;
            this.content = content;
            this.entityConstructor = entityConstructor;
        }

        public Entity Make()
        {
            Sprite npcSprite = new Sprite(content.Load<Texture2D>("Sprites\\NpcEnemy"), new Vector2(16f, 16f));

            return new NpcEnemy(npcSprite, entityConstructor, stats)
            {
                CanCollide = true,
                IsPhysical = true,
                CollisionRadius = 0.6f
            };
        }
    }
}