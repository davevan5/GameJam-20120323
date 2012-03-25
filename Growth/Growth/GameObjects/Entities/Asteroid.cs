using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Growth.GameObjects.Entities
{
    public class Asteroid : Entity
    {
        EntityConstructor entityContructor;
        private static Random rand = new Random();
        private const int maxHealth = 500;
        private const int explosiveFactor = 10;
        public int DropCount;
        public int Health;

        public Asteroid(Sprite sprite, EntityConstructor entityContructor)
            : base(sprite)
        {
            this.entityContructor = entityContructor;
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
                Ore newOre = (Ore)entityContructor.MakeEntity(typeof(Ore));
                newOre.Position = Position;
                newOre.Velocity = new Vector2(((float)rand.NextDouble() * 2 - 1) * explosiveFactor, ((float)rand.NextDouble() * 2 - 1) * explosiveFactor);
            }
            
            base.OnDestroyed();
        }
    }
}