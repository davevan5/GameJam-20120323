using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Growth.GameObjects.Entities
{
    public class Asteroid : Entity
    {
        private static Random rand = new Random();
        private const int maxHealth = 500;
        public int DropCount;
        public int Health;

        public Asteroid(Sprite sprite)
            : base(sprite)
        {
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
    }
}