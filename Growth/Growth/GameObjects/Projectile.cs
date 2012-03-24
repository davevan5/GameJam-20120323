using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Growth.GameObjects
{
    public class Projectile : Entity
    {
        private const float DragFactor = 0.98f;
        private const float Speed = 50f;
        public Vector2 Velocity;

        public Projectile(Sprite sprite)
            : base(sprite)
        {

        }

        public override void Update(double dt)
        {
            Move(dt);
        }

        public void Move(double dt)
        {
            Sprite.Position = Position += Velocity * (float)dt;
            Sprite.Rotation = Rotation;
        }

        public void SetDirection(Vector2 direction)
        {
            Velocity = direction * Speed;
            Rotation = (float)Math.Atan2(direction.Y, direction.X);
        }
    }
}
