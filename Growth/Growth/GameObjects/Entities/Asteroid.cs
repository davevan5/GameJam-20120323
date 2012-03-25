using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Growth.GameObjects.Entities
{
    public class Asteroid : Entity
    {
        private const int maxHealth = 500;
        public int Health;

        public Asteroid(Sprite sprite)
            : base(sprite)
        {
            Health = 500;
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