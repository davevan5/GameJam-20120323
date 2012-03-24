using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Growth.GameObjects
{
    public class Ship : Entity
    {
        public Vector2 Velocity;
        public int Shield;
        public int Health;

        public void Update(double dt)
        {
            Position += Velocity * (float)dt;
        }
    }
}