using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Growth
{
    public class Ship
    {
        public Texture2D Texture;
        public Vector2 Position;
        public Vector2 Velocity;
        public float Rotation;

        public int Health;

        public void Update(double dt)
        {
            Position = Velocity * (float)dt;
        }
    }
}