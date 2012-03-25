using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Growth.GameObjects
{
    public class Projectile : Entity
    {        
        private const float Speed = 50f;
        private const double Lifespan = 1;
        private double timeAlive;        
        public bool DoDispose;

        public Projectile(Sprite sprite)
            : base(sprite)
        {
            DragFactor = 0.98f;
        }

        public override void Update(double dt)
        {
            Move(dt);
            timeAlive += dt;
            if (timeAlive >= Lifespan)
                OnDestroyed();
        }

        public void Move(double dt)
        {
            Position += Velocity * (float)dt;            
        }

        public void SetDirection(Vector2 direction)
        {
            Velocity = direction * Speed;
            Rotation = (float)Math.Atan2(direction.Y, direction.X);
        }

        public override void CollisionWith(Entity collider)
        {
            if (collider is Ship)
                return;

            OnDestroyed();

            base.CollisionWith(collider);
        }
    }
}
