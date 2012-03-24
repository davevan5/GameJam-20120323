using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Growth.GameObjects
{
    public class Planet : Entity
    {
        public int Health;

        public Planet(Sprite sprite)
            : base(sprite)
        { }

        public override void Update(double dt)
        {
            Position = Sprite.Position = Position;
        }
    }
}