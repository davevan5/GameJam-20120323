using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Growth.GameObjects.Entities
{
    public class Asteroid : Entity
    {
        private const int ScoreReward = 100;        
        private const int maxHealth = 200;
        private const int explosiveFactor = 10;

        private static Random rand = new Random();

        private readonly PlayerStats stats;

        EntityConstructor entityConstructor;

        public int DropCount;
        public int Health;
        
        
        public Asteroid(Sprite sprite, EntityConstructor entityConstructor, PlayerStats stats)
            : base(sprite)
        {
            this.stats = stats;
            this.entityConstructor = entityConstructor;
            Health = maxHealth;
            DropCount = rand.Next(1, 5);
        }

        public override void Update(double dt)
        {
            Sprite.Rotation = this.Rotation += 0.3f * (float)dt;

            if (Health < 1)
                OnDestroyed();
        }

        public override void CollisionWith(Entity collider)
        {
            if (collider is Projectile)
                Health -= ((Projectile)collider).Damage;

            base.CollisionWith(collider);
        }

        protected override void OnDestroyed()
        {
            int maxDropDistance = (int)(CollisionRadius * 2);
            for (int i = 0; i < DropCount; i++)
            {
                Ore newOre = (Ore)entityConstructor.MakeEntity(typeof(Ore));
                newOre.Position = Position;
                newOre.Velocity = new Vector2(((float)rand.NextDouble() * 2 - 1) * explosiveFactor, ((float)rand.NextDouble() * 2 - 1) * explosiveFactor);
            }

            stats.Score += ScoreReward;

            base.OnDestroyed();
        }
    }
}