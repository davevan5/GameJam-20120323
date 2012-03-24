using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Growth.GameObjects
{
    public class Asteroid : Entity
    {
        public Vector2 Velocity;
        public int Health;

        public Asteroid(Sprite sprite)
            : base(sprite)
        { }

        public override void Update(double dt)
        {
        }
    }
}