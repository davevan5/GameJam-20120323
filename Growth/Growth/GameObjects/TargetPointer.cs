using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Growth.GameObjects
{
    public class TargetPointer
    {
        private Entity target;
        private Ship ship;
        private Sprite sprite;
        public Vector2 Position;
        public float Rotation;

        public TargetPointer(Entity target, Ship ship, Sprite sprite)
        {
            this.ship = ship;
            this.target = target;
            this.sprite = sprite;
        }

        public void Update()
        {
            Vector2 direction = target.Position - ship.Position;
            float distance = Vector2.Distance(target.Position, ship.Position);

            if (direction != Vector2.Zero)
                direction.Normalize();

            if (distance > 100f)
                distance = 100f;
            else if (distance < 15f)
                distance = 15f;

            sprite.Rotation = this.Rotation = (float)Math.Atan2(direction.Y, direction.X);
            sprite.Position = this.Position = (ship.Position + direction * (distance * 0.1f));
        }
    }
}
